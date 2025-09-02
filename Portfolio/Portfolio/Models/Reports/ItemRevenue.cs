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
        public List<ItemPriceReport>? Prices { get; set; }
        public decimal TotalRevenue { get; set; }
    }
}
