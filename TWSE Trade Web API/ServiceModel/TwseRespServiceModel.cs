namespace TWSE_Trade_Web_API.ServiceModel
{
    public class TwseRespServiceModel
    {
        /* code
         *    0 : Update Sucess
         *    9 : Twse API return No data
         */
        public int code { get; set; }
        public string message { get; set; }
    }
}
