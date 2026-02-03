namespace Cafe.Core.DTOs.Filters
{
    /// <summary>
    /// Used for filtering category sales reports.
    /// </summary>
    public class CategoryItemFilter
    {
        public string? ItemName { get; set; }
        public List<ItemReportFilter>? ItemReports { get; set; }
    }
}