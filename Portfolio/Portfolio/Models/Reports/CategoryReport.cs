namespace Portfolio.Models.Reports
{
    /// <summary>
    /// Used to display sales reports concerning all items from a particular category.
    /// </summary>
    public class CategoryReport
    {
        /// <summary>
        /// The name of the item sold.
        /// </summary>
        public string? ItemName { get; set; }

        /// <summary>
        /// A list of reports for each date that this item sold. Includes price, quantity, and extended price.
        /// </summary>
        public List<ItemReport>? ItemDateReports { get; set; }
    }
}