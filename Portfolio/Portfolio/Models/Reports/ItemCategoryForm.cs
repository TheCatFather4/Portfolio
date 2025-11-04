using Microsoft.AspNetCore.Mvc.Rendering;

namespace Portfolio.Models.Reports
{
    public class ItemCategoryForm
    {
        public SelectList? Items { get; set; }
        public int? SelectedItemID { get; set; }
        public List<ItemReport>? ItemReports { get; set; }
        public SelectList? Categories { get; set; }
        public int? SelectedCategoryID { get; set; }
        public List<CategoryReport> CategoryReports { get; set; }
        public int TotalQuantity { get; set; } = 0;
        public decimal TotalRevenue { get; set; } = 0;
    }
}