namespace TWSE_Trade_Web_API.ServiceModel
{
    public class StockRespServiceModel
    {
        public string StockId { get; set; }
        public string Name { get; set; }
        public string LatestTradeDate { get; set; }
        public float LatestClosingPrice { get; set; }
    }
}
