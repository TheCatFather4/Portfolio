using Microsoft.AspNetCore.Mvc.Rendering;

namespace Portfolio.Models.Cafe.Reports
{
    /// <summary>
    /// Used to display sales data by item or by category.
    /// </summary>
    public class ItemCategoryForm
    {
        /// <summary>
        /// A select list of items to filter sales reports.
        /// </summary>
        public SelectList? Items { get; set; }

        /// <summary>
        /// The ID of the selected item from the SelectList of items.
        /// </summary>
        public int? SelectedItemID { get; set; }

        /// <summary>
        /// A list of sales reports to display to the user. The reports are based upon the selected item's ID.
        /// </summary>
        public List<ItemReport>? ItemReports { get; set; }

        /// <summary>
        /// A select list of categories to filter sales reports.
        /// </summary>
        public SelectList? Categories { get; set; }

        /// <summary>
        /// The ID of the selected category from the SelectList of categories.
        /// </summary>
        public int? SelectedCategoryID { get; set; }

        /// <summary>
        /// A list of sales reports to display to the user. The reports are based upon the selected category ID.
        /// </summary>
        public List<CategoryReport>? CategoryReports { get; set; }

        /// <summary>
        /// The quantity of the item that sold.
        /// </summary>
        public int TotalQuantity { get; set; } = 0;

        /// <summary>
        /// The total revenue for sales in accord with the selected filter.
        /// </summary>
        public decimal TotalRevenue { get; set; } = 0;
    }
}