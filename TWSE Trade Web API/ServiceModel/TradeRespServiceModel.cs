using System;

namespace TWSE_Trade_Web_API.ServiceModel
{
    public class TradeRespServiceModel
    {
        public int Id { get; set; }
        public string StockId { get; set; }
        public string StockName { get; set; }
        public DateTime TradeDate { get; set; }
        public char Type { get; set; }
        public int Volume { get; set; }
        public float Fee { get; set; }
        public float ClosingPrice { get; set; }
        public int LendingPeriod { get; set;}
    }
}
