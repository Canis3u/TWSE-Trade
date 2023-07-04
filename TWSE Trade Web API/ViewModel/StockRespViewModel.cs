using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TWSE_Trade_Web_API.ViewModel
{
    public class StockRespViewModel
    {
        public string StockId { get; set; }
        public string Name { get; set; }
        public string LatestTradeDate { get; set; }
        public float LatestClosingPrice { get; set; }
    }
}
