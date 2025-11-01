namespace Cafe.Core.DTOs
{
    public class ItemReportFilter
    {
        public int SelectedItemID { get; set; }
        public List<ItemDateReportFilter> Dates { get; set; }
        public int TotalQuantity { get; set; }
        public decimal TotalRevenue { get; set; }
    }
}