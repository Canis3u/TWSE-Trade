using System.Collections.Generic;

namespace TWSE_Trade_Web_API.ServiceModel
{
    public class TradeQueryRespServiceModel
    {
        public TradeQueryServiceModel QueryParams { get; set; }
        public int TotalCount { get; set; }
        public List<TradeRespServiceModel> Items { get; set; }
    }
}
