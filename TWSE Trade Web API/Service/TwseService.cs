using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
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
        public async Task<int> UpdateDatabaseAsync(string endDate)
        {
            // var startDate = _twseTradeContext.Trades.OrderByDescending(x=>x.TradeDate).FirstOrDefault()?.TradeDate.AddDays(1).ToString();
            var startDate = "20230101";
            var responseBodyString = await GetDataFromAPI(startDate??"20230101",endDate);
            var tqvm = ExtractData(responseBodyString);
            if (tqvm.data != null && tqvm.data.Any())
            {
                var stockList = _mapper.Map<List<Stock>>(tqvm.data);
                var closingPriseList = _mapper.Map<List<ClosingPrice>>(tqvm.data);
                var tradeList = _mapper.Map<List<Trade>>(tqvm.data);
                var rowschanges = await InsertToDBAsync(stockList, closingPriseList, tradeList);
                Console.WriteLine(rowschanges);
            }
            else
            {
                Console.WriteLine("No data");
            }
            return 0;
        }
        private async Task<int> InsertToDBAsync(List<Stock> stockList, List<ClosingPrice> closingPriceList, List<Trade> tradeList)
        {
            /* 等我哪天長大了, 再看看能不能想到
             Stock
            var stockIds = stockList.Select(x => x.StockId).Distinct(); // 列出所有要加入DB的Stock的stockID,並用Distinct去除重複值
            var DBStockIds = _twseTradeContext.Stocks.Where(x => stockIds.Contains(x.StockId)).Select(x => x.StockId); // 找出要加入但已經存在DB的stockID
            var stocks = new List<Stock>();
            var addStockIds = new HashSet<string>();
            foreach (Stock stock in stockList.Where(x => !DBStockIds.Contains(x.StockId)))
            {
                if (!addStockIds.Contains(stock.StockId))
                {
                    stocks.Add(stock);
                    addStockIds.Add(stock.StockId);
                }
            }
            _twseTradeContext.Stocks.AddRange(stocks);

            //// ClosingPrice
            var closingPriceSets = closingPriceList.Select(x => new { x.StockId, x.TradeDate });
            var DBclosingPriceSets = _twseTradeContext.ClosingPrices.Where(x => closingPriceSets.Contains(new { x.StockId, x.TradeDate })).Select(x => new { x.StockId, x.TradeDate });
            var closingPrices = new List<ClosingPrice>();
            var addClosingPriceIds = new HashSet<string>();
            foreach (ClosingPrice closingPrice in closingPriceList.Where(x => !DBclosingPriceSets.Contains(new { x.StockId, x.TradeDate })))
            {
                if (!addClosingPriceIds.Contains(new { closingPrice.StockId, closingPrice.TradeDate }))
                {
                    await _twseTradeContext.ClosingPrices.AddAsync(closingPrice);
                }
            }
            */
            // Stock
            var addStockIds = new HashSet<string>();
            foreach (Stock stock in stockList)
            {
                if (_twseTradeContext.Stocks.Where(x => x.StockId == stock.StockId).FirstOrDefault() == null && !addStockIds.Contains(stock.StockId))
                {
                    await _twseTradeContext.AddAsync(stock);
                    addStockIds.Add(stock.StockId);
                }
            }
            var srch = _twseTradeContext.SaveChanges();
            // ClosingPrice
            var addClosingPriceStrings = new HashSet<string>();
            foreach (ClosingPrice closingPrice in closingPriceList)
            {
                if (_twseTradeContext.ClosingPrices.Where(x => x.StockId== closingPrice.StockId && x.TradeDate== closingPrice.TradeDate).FirstOrDefault()==null)
                {
                    var ClosingPriceString = $"{closingPrice.StockId}{closingPrice.TradeDate}";
                    if (!addClosingPriceStrings.Contains(ClosingPriceString))
                    {
                        await _twseTradeContext.AddAsync(closingPrice);
                        addClosingPriceStrings.Add(ClosingPriceString);
                    }
                }
            }
            var crch = _twseTradeContext.SaveChanges();

            // Trade
            await _twseTradeContext.Trades.AddRangeAsync(tradeList);
            var trch = _twseTradeContext.SaveChanges();
            return srch + crch + trch;
        }
        static private async Task<string> GetDataFromAPI(string startDate, string endDate)
        {
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
