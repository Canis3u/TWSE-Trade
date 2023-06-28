using System.Collections.Generic;

namespace TWSE_Trade_Web_API.ViewModel
{
    public class TradeQueryRespViewModel
    {
        public TradeQueryViewModel QueryParams { get; set; }
        public int TotalCount { get; set; }
        public List<TradeRespViewModel> Items { get; set; }
    }
}
