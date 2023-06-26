using System.Threading.Tasks;
using TWSE_Trade_Web_API.ServiceModel;

namespace TWSE_Trade_Web_API.Helpers.Interface
{
    public interface ITwseRequestHelper
    {
        public Task<TwseServiceModel> RequestToTwseAPIAsync(string startDate, string endDate);
    }
}
