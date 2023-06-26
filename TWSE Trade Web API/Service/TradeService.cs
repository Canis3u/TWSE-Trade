using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TWSE_Trade_Web_API.Models;
using TWSE_Trade_Web_API.Service.Interface;
using TWSE_Trade_Web_API.ServiceModel;

namespace TWSE_Trade_Web_API.Service
{
    public class TradeService:ITradeService
    {
        private readonly TWSETradeContext _twseTradeContext;
        private readonly IMapper _mapper;
        public TradeService(TWSETradeContext twseTradeContext, IMapper mapper)
        {
            _twseTradeContext = twseTradeContext;
            _mapper = mapper;
        }

        public async Task<TradeRespServiceModel> SelectTradeByIdAsync(int id)
        {
            var entity = await _twseTradeContext.Trades.Where(x => x.Id == id)
                                                 .Include(x => x.Stock)
                                                 .ThenInclude(x => x.ClosingPrices).ToListAsync();
            return new TradeRespServiceModel() { };
        }
    }
}
