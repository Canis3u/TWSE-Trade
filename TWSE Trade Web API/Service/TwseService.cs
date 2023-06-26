using AutoMapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Transactions;
using TWSE_Trade_Web_API.Models;
using TWSE_Trade_Web_API.Service.Interface;
using TWSE_Trade_Web_API.ViewModel;

namespace TWSE_Trade_Web_API.Service
{
    public class TwseService : ITwseService
    {
        public IConfiguration _configuration { get; }
        private readonly TWSETradeContext _twseTradeContext;
        private readonly IMapper _mapper;
        public TwseService(IConfiguration configuration, TWSETradeContext twseTradeContext, IMapper mapper)
        {
            _configuration = configuration;
            _twseTradeContext = twseTradeContext;
            _mapper = mapper;
        }
        public async Task<String> UpdateDatabaseAsync(string endDate)
        {
            var startDate = _twseTradeContext.Trades.OrderByDescending(x=>x.TradeDate).FirstOrDefault()?.TradeDate.AddDays(1).ToString("yyyyMMdd");
            var responseBodyString = await GetDataFromAPIAsync(startDate??_configuration.GetValue<string>("StartDate"), endDate);
            var tqvm = ExtractData(responseBodyString);
            if (tqvm.data != null && tqvm.data.Any())
            {
                var stockList = _mapper.Map<List<Stock>>(tqvm.data);
                var closingPriseList = _mapper.Map<List<ClosingPrice>>(tqvm.data);
                var tradeList = _mapper.Map<List<Trade>>(tqvm.data);
                var rowschanges = await InsertToDBAsync(stockList, closingPriseList, tradeList, "Admin");
                return $"Total {rowschanges} changes.";
            }
            else
            {
                return "TWSE No data";
            }
        }
        private async Task<int> InsertToDBAsync(List<Stock> stockList, List<ClosingPrice> closingPriceList, List<Trade> tradeList, string userName)
        {
            int rowschanges = 0;
            using (TransactionScope ts = new TransactionScope())
            {
                // Clean StockList
                var stockIdsInDB = _twseTradeContext.Stocks.Select(x => x.StockId).ToList(); // 找出已經存在DB的stockID
                var stocks = new List<Stock>(); // 用個list存要加入的stock
                foreach (Stock stock in stockList)
                {
                    if (!stockIdsInDB.Contains(stock.StockId))
                    {
                        stocks.Add(stock);
                        stockIdsInDB.Add(stock.StockId);
                    }
                }

                // Clean ClosingPriceList
                // 因為一次要比兩個欄位, 乾脆直接組合成字串
                var closingPriceStringsInDB = _twseTradeContext.ClosingPrices.Select(x => $"{x.StockId}{x.TradeDate.ToShortDateString()}").ToList(); // 找出已經存在DB的字串組合
                var closingPrices = new List<ClosingPrice>(); // 用個list存要加入的ClosingPrice
                foreach (ClosingPrice closingPrice in closingPriceList)
                {
                    var closingPricestring = $"{closingPrice.StockId}{closingPrice.TradeDate.ToShortDateString()}";
                    if (!closingPriceStringsInDB.Contains(closingPricestring))
                    {
                        closingPrices.Add(closingPrice);
                        closingPriceStringsInDB.Add(closingPricestring);
                    }
                }

                // 押上使用者與時間
                stocks.ForEach(x => {
                    x.CreateUser = userName;
                    x.CreateDate = DateTime.Now;
                });
                closingPrices.ForEach(x => {
                    x.CreateUser = userName;
                    x.CreateDate = DateTime.Now;
                });
                tradeList.ForEach(x => {
                    x.CreateUser = userName;
                    x.CreateDate = DateTime.Now;
                });

                // Trade很棒都不用處理
                await _twseTradeContext.Stocks.AddRangeAsync(stocks);
                await _twseTradeContext.ClosingPrices.AddRangeAsync(closingPrices);
                await _twseTradeContext.Trades.AddRangeAsync(tradeList);
                rowschanges = _twseTradeContext.SaveChanges();
                ts.Complete();
            }
            return rowschanges;            

        }
        private async Task<string> GetDataFromAPIAsync(string startDate, string endDate)
        {
            Console.WriteLine($"startDate={startDate}&endDate={endDate}");
            var url = _configuration.GetValue<string>("TWSETrade") + $"?startDate={startDate}&endDate={endDate}&tradeType=&stockNo=&response=json";
            var responseString = "";
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                responseString = await response.Content.ReadAsStringAsync();
            }
            return responseString;
        }
        static private TwseReqViewModel ExtractData(string responseBodyString)
        {
            var tqvm = JsonSerializer.Deserialize<TwseReqViewModel>(responseBodyString);
            return tqvm;
        }       
    }
}
