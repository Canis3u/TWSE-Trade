using Microsoft.Extensions.Configuration;
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
            var tqvm = JsonSerializer.Deserialize<TwseServiceModel>(responseString,options);
            return tqvm;
        }
    }
}
