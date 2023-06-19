using System;
using System.Collections.Generic;

#nullable disable

namespace TWSE_Trade_Web_API.Models
{
    public partial class Stock
    {
        public Stock()
        {
            ClosingPrices = new HashSet<ClosingPrice>();
            Trades = new HashSet<Trade>();
        }

        public string StockId { get; set; }
        public string Name { get; set; }
        public string CreateUser { get; set; }
        public DateTime? CreateDate { get; set; }
        public string UpdateUser { get; set; }
        public DateTime? UpdateDate { get; set; }

        public virtual ICollection<ClosingPrice> ClosingPrices { get; set; }
        public virtual ICollection<Trade> Trades { get; set; }
    }
}
