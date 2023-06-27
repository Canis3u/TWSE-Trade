using AutoMapper;
using System.Threading.Tasks;
using TWSE_Trade_Web_API.Models;
using TWSE_Trade_Web_API.Repositories;
using TWSE_Trade_Web_API.Service.Interface;
using TWSE_Trade_Web_API.ServiceModel;

namespace TWSE_Trade_Web_API.Service
{
    public class TradeService:ITradeService
    {
        private readonly TWSETradeContext _twseTradeContext;
        private readonly IMapper _mapper;
        private readonly TradeRepository _tradeRepository;

        public TradeService(TWSETradeContext twseTradeContext, IMapper mapper, TradeRepository tradeRepository)
        {
            _twseTradeContext = twseTradeContext;
            _mapper = mapper;
            _tradeRepository = tradeRepository;
        }

        public async Task<TradeRespServiceModel> ReadTradeInformatoinByIdAsync(int id)
        {
            var entity = await _tradeRepository.SelectTradeInformatoinByIdAsync(id);
            return entity;
        }
    }
}
