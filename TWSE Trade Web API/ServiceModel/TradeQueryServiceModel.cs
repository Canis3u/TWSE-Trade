namespace TWSE_Trade_Web_API.ServiceModel
{
    public class TradeQueryServiceModel
    {
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string TradeType { get; set; }
        public string StockId { get; set; }
        public string SortColumn { get; set; }
        public string SortDirection { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
    }
}
