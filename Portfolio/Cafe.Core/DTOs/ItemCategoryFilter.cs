namespace Cafe.Core.DTOs
{
    public class ItemCategoryFilter
    {
        public int SelectedItemID { get; set; }
        public List<ItemFilter>? Items { get; set; }
        public int SelectedCategoryID { get; set; }
        public List<CategoryFilter>? Categories { get; set; }
        public int TotalQuantity { get; set; }
        public decimal TotalRevenue { get; set; }
    }
}