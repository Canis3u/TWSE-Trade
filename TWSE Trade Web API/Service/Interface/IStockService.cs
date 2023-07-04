using System.Threading.Tasks;
using TWSE_Trade_Web_API.ServiceModel;

namespace TWSE_Trade_Web_API.Service.Interface
{
    public interface IStockService
    {
        Task<StockRespServiceModel> ReadStockInformatoinByStockIdAsync();
    }
}
