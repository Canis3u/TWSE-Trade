using Microsoft.Extensions.Configuration;
using System;
using System.Globalization;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using TWSE_Trade_Web_API.Helpers.Interface;
using TWSE_Trade_Web_API.ServiceModel;

namespace TWSE_Trade_Web_API.Helpers
{
    public class TwseRequestHelper:ITwseRequestHelper
    {
        public IConfiguration _configuration { get; }
        public TwseRequestHelper(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<TwseServiceModel> RequestToTwseAPIAsync(string startDate, string endDate)
        {
            var url = _configuration.GetValue<string>("TWSETrade") + 
                      $"?startDate={startDate}&endDate={endDate}" +
                      $"&tradeType=&stockNo=&response=json";
            var responseString = "";
            // Request to url
             HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
                responseString = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            // Response string to ServiceModel
            var serviceModel = JsonSerializer.Deserialize<TwseServiceModel>(responseString,options);
            ValidTradeDate(serviceModel, startDate, endDate);
            return serviceModel;
        }
        private void ValidTradeDate(TwseServiceModel serviceModel, string startDate, string endDate)
        {
            CultureInfo culture = new CultureInfo("zh-TW");
            culture.DateTimeFormat.Calendar = new TaiwanCalendar();
            DateTime date;
            foreach (var d in serviceModel.data)
            {
                try
                {
                    date = DateTime.Parse(d[0].ToString(), culture);
                }
                catch (Exception)
                {
                    throw new Exception("Date Format Error.");
                }
                // Danny寫ㄉ
                int ddate = int.Parse(date.ToString("yyyyMMdd"));
                int sdate = int.Parse(startDate);
                int edate = int.Parse(endDate);
                // 到這都是
                if (ddate < sdate || ddate > edate)
                    throw new Exception("Date out of range.");
            }
        }
    }
}
