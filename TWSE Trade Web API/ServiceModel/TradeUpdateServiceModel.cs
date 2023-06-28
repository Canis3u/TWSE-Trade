namespace TWSE_Trade_Web_API.ServiceModel
{
    public class TradeUpdateServiceModel
    {
        public string Type { get; set; }
        public int Volume { get; set; }
        public float Fee { get; set; }
        public int LendingPeriod { get; set; }
        public int Status { get; set; }
    }
}
