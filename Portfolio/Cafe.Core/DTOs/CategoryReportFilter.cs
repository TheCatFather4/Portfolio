namespace Cafe.Core.DTOs
{
    public class CategoryReportFilter
    {
        public string ItemName { get; set; }
        public List<ItemDateReportFilter> ItemReports { get; set; }
    }
}