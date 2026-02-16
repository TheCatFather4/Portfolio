namespace Portfolio.Models.Cafe.Reports
{
    /// <summary>
    /// Used to display sales data concerning an item.
    /// </summary>
    public class ItemReport
    {
        /// <summary>
        /// The item's price.
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// The quantity sold.
        /// </summary>
        public byte Quantity { get; set; }

        /// <summary>
        /// The item's price mutiplied by the quantity sold.
        /// </summary>
        public decimal ExtendedPrice { get; set; }

        /// <summary>
        /// The date that the item sold.
        /// </summary>
        public DateTime DateSold { get; set; }
    }
}