using System;
using System.Collections.Generic;

#nullable disable

namespace TWSE_Trade_Web_API.Models
{
    public partial class ClosingPrice
    {
        public string StockId { get; set; }
        public DateTime TradeDate { get; set; }
        public double? Price { get; set; }
        public string CreateUser { get; set; }
        public DateTime? CreateDate { get; set; }
        public string UpdateUser { get; set; }
        public DateTime? UpdateDate { get; set; }

        public virtual Stock Stock { get; set; }
    }
}
