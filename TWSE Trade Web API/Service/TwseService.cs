using AutoMapper;
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
        private readonly TWSETradeContext _twseTradeContext;
        private readonly IMapper _mapper;
        public TwseService(TWSETradeContext twseTradeContext, IMapper mapper)
        {
            _twseTradeContext = twseTradeContext;
            _mapper = mapper;
        }
        public async Task<String> UpdateDatabaseAsync(string endDate)
        {
            var startDate = _twseTradeContext.Trades.OrderByDescending(x=>x.TradeDate).FirstOrDefault()?.TradeDate.AddDays(1).ToString("yyyyMMdd");
            var responseBodyString = await GetDataFromAPI(startDate??"20230101",endDate);
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
                var stockIdsInDB = _twseTradeContext.Stocks.Select(x => x.StockId).ToList(); // 找出要加入但已經存在DB的stockID
                var stocks = new List<Stock>();
                foreach (Stock stock in stockList)
                {
                    if (!stockIdsInDB.Contains(stock.StockId))
                    {
                        stocks.Add(stock);
                        stockIdsInDB.Add(stock.StockId);
                    }
                }

                // Clean ClosingPriceList
                var closingPriceStringsInDB = _twseTradeContext.ClosingPrices.Select(x => $"{x.StockId}{x.TradeDate.ToShortDateString()}").ToList();
                var closingPrices = new List<ClosingPrice>();
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
        static private async Task<string> GetDataFromAPI(string startDate, string endDate)
        {
            Console.WriteLine($"startDate={startDate}&endDate={endDate}");
            var uri = $"https://www.twse.com.tw/rwd/zh/lending/t13sa710?startDate={startDate}&endDate={endDate}&tradeType=&stockNo=&response=json";
            var responseString = "";
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(uri);
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
