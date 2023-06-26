using AutoMapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using TWSE_Trade_Web_API.Helpers.Interface;
using TWSE_Trade_Web_API.Models;
using TWSE_Trade_Web_API.Service.Interface;
using TWSE_Trade_Web_API.ServiceModel;

namespace TWSE_Trade_Web_API.Service
{
    public class TwseService : ITwseService
    {
        public IConfiguration _configuration { get; }
        private readonly ITwseRequestHelper _twseRequestHelper;
        private readonly TWSETradeContext _twseTradeContext;
        private readonly IMapper _mapper;
        public TwseService(IConfiguration configuration, ITwseRequestHelper twseRequestHelper, TWSETradeContext twseTradeContext, IMapper mapper)
        {
            _configuration = configuration;
            _twseRequestHelper = twseRequestHelper;
            _twseTradeContext = twseTradeContext;
            _mapper = mapper;
        }
        public async Task<TwseRespServiceModel> UpdateDBFromTwseAPIAsync(string endDate)
        {
            var startDate = _twseTradeContext.Trades.OrderByDescending(x=>x.TradeDate).FirstOrDefault()?.TradeDate.AddDays(1).ToString("yyyyMMdd");
            var tqvm = await _twseRequestHelper.RequestToTwseAPIAsync(startDate??_configuration.GetValue<string>("StartDate"), endDate);
            var trsm = new TwseRespServiceModel();
            if (tqvm.data != null && tqvm.data.Any())
            {
                var stockList = _mapper.Map<List<Stock>>(tqvm.data);
                var closingPriseList = _mapper.Map<List<ClosingPrice>>(tqvm.data);
                var tradeList = _mapper.Map<List<Trade>>(tqvm.data);
                var rowschanges = await InsertToDBAsync(stockList, closingPriseList, tradeList, "Admin");
                trsm.code = 0;
                trsm.message = $"Total {rowschanges} changes.";
            }
            else
            {
                trsm.code = 9;
                trsm.message = $"Twse API No Data";
            }
            return trsm;
        }
        private async Task<int> InsertToDBAsync(List<Stock> stockList, List<ClosingPrice> closingPriceList, List<Trade> tradeList, string userName)
        {
            int rowschanges = 0;
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

            using (TransactionScope ts = new TransactionScope())
            {
                // Trade很棒都不用處理
                await _twseTradeContext.Stocks.AddRangeAsync(stocks);
                await _twseTradeContext.ClosingPrices.AddRangeAsync(closingPrices);
                await _twseTradeContext.Trades.AddRangeAsync(tradeList);
                rowschanges = _twseTradeContext.SaveChanges();
                ts.Complete();
            }
            return rowschanges;            
        }
    }
}
