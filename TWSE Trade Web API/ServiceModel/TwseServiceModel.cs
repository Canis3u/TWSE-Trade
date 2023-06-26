using System.Collections.Generic;

namespace TWSE_Trade_Web_API.ServiceModel
{
    public class TwseServiceModel
    {
        public string stat { get; set; }
        public string title { get; set; }
        public List<string> fields { get; set; }
        public List<List<object>> data { get; set; }
        public List<string> notes { get; set; }
        public Param Params { get; set; }
    }
    public class Param
    {
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string TradeType { get; set; }
        public string StockNo { get; set; }
        public string Response { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
        public string Lang { get; set; }
    }
}
