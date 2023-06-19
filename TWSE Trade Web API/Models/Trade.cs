using System;
using System.Collections.Generic;

#nullable disable

namespace TWSE_Trade_Web_API.Models
{
    public partial class Trade
    {
        public int Id { get; set; }
        public string StockId { get; set; }
        public DateTime TradeDate { get; set; }
        public string Type { get; set; }
        public int Volume { get; set; }
        public double Fee { get; set; }
        public int LendingPeriod { get; set; }
        public int Status { get; set; }
        public string CreateUser { get; set; }
        public DateTime? CreateDate { get; set; }
        public string UpdateUser { get; set; }
        public DateTime? UpdateDate { get; set; }

        public virtual Stock Stock { get; set; }
    }
}
