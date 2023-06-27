using System.Threading.Tasks;
using TWSE_Trade_Web_API.ServiceModel;

namespace TWSE_Trade_Web_API.Service.Interface
{
    public interface ITradeService
    {
        Task<TradeRespServiceModel> ReadTradeInformatoinByIdAsync(int id);
        Task<int> UpdateTradeByIdAsync(int id, string user, TradeServiceModel sm);
        Task<int> UpdateTradeStatusWithDeletedCodeByIDAsync(int id, string user);
    }
}
