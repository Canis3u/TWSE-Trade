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

        public async Task<TradeQueryRespServiceModel> ReadTradesInformationAsync(TradeQueryServiceModel serviceModel)
        {
            var items = await _tradeRepository.SelectTradesInformationAsync(serviceModel);
            var count = await _tradeRepository.SelectTradesCountAsync(serviceModel);
            var respServiceModel = new TradeQueryRespServiceModel() { Items = items, TotalCount = count, QueryParams = serviceModel };
            return respServiceModel;
        }
        public async Task<TradeRespServiceModel> ReadTradeInformatoinByIdAsync(int id)
        {
            var item = await _tradeRepository.SelectTradeInformatoinByIdAsync(id);
            return item;
        }

        public async Task<int> UpdateTradeByIdAsync(int id, string user, TradeUpdateServiceModel serviceModel)
        {
            var trade = _mapper.Map<Trade>(serviceModel);
            var rowschange = await _tradeRepository.UpdateTradeByIdAsync(id,user,trade);
            return rowschange;
        }
        public async Task<int> UpdateTradeStatusWithDeletedCodeByIDAsync(int id, string user)
        {
            var rowschange = await _tradeRepository.UpdateTradeStatusWithDeletedCodeByIdAsync(id,user);
            return rowschange;
        }
    }
}
