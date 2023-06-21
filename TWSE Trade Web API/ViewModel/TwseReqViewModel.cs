using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TWSE_Trade_Web_API.ViewModel
{
    public class TwseReqViewModel
    {
        public string stat { get; set; }
        public string title { get; set; }
        public List<string> fields { get; set; }
        public List<List<object>> data { get; set; }
        public List<string> notes { get; set; }
    }

    public class Params
    {
        public string StartDate {get;set;}
        public string EndDate {get;set;}
        public string TradeType {get;set;}
        public string StockNo {get;set;}
        public string Response {get;set;}
        public string Controller {get;set;}
        public string Action {get;set;}
        public string Lang {get;set;}
    }
}
