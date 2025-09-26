using Cafe.Core.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Portfolio.Models.Reports
{
    public class ItemRevenue
    {
        public SelectList? Items { get; set; }
        public int? SelectedItemID { get; set; }
        public SelectList? Categories { get; set; }
        public int? SelectedCategoryID { get; set; }
        public List<ItemDateReport>? Dates { get; set; }
        public int TotalQuantity { get; set; } = 0;
        public decimal TotalRevenue { get; set; } = 0;
        public List<CategoryReport> CategoryReports { get; set; }
    }
}
