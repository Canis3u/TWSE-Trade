using System;

namespace TWSE_Trade_Web_API.ViewModel
{
    public class TradeRespViewModel
    {
        public int Id { get; set; }
        public string StockIdAndName { get; set; }
        public string TradeDate { get; set; }
        public string Type { get; set; }
        public int Volume { get; set; }
        public string Fee { get; set; }
        public string ClosingPrice { get; set; }
        public int LendingPeriod { get; set; }
        public string ReturnDate { get; set; }
    }
}
