namespace Cafe.Core.DTOs
{
    public class ItemCategoryFilter
    {
        public int SelectedItemID { get; set; }
        public List<ItemReport>? Reports { get; set; }
        public int SelectedCategoryID { get; set; }
        public List<CategoryItem>? CategoryItems { get; set; }
        public int TotalQuantity { get; set; }
        public decimal TotalRevenue { get; set; }
    }
}