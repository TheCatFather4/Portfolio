namespace Cafe.Core.DTOs.Filters
{
    /// <summary>
    /// Used to filter and map sales data concerning items or categories.
    /// </summary>
    public class ItemCategoryFilter
    {
        public int SelectedItemID { get; set; }
        public List<ItemReportFilter>? Reports { get; set; }
        public int SelectedCategoryID { get; set; }
        public List<CategoryItemFilter>? CategoryItems { get; set; }
        public int TotalQuantity { get; set; }
        public decimal TotalRevenue { get; set; }
    }
}