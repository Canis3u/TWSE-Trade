using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TWSE_Trade_Web_API.ServiceModel;

namespace TWSE_Trade_Web_API.Service.Interface
{
    public interface ITradeService
    {
        Task<TradeRespServiceModel> SelectTradeByIdAsync(int id);
    }
}
