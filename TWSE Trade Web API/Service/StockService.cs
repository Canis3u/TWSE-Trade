using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using TWSE_Trade_Web_API.Models;
using TWSE_Trade_Web_API.Service.Interface;
using TWSE_Trade_Web_API.ServiceModel;

namespace TWSE_Trade_Web_API.Service
{
    public class StockService:IStockService
    {
        private readonly TWSETradeContext _twseTradeContext;
        private readonly IMapper _mapper;
        public StockService(TWSETradeContext twseTradeContext, IMapper mapper)
        {
            _twseTradeContext = twseTradeContext;
            _mapper = mapper;
        }

        public async Task<StockRespServiceModel> ReadStockInformatoinByStockIdAsync(string stockId)
        {
            // 以Stock為主表去找 沒有辦法一次做到, 所以反向以ClosingPrice去查詢
            var closingPrice = await _twseTradeContext.ClosingPrices
                .Where(x => x.StockId == stockId)
                .Include(x => x.Stock)
                .OrderByDescending(x => x.TradeDate)
                .FirstOrDefaultAsync();
            var respServiceModel = _mapper.Map<StockRespServiceModel>(closingPrice);
            return respServiceModel;

        }
    }
}
