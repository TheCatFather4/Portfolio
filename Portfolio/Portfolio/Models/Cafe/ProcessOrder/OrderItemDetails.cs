namespace Portfolio.Models.Cafe.ProcessOrder
{
    /// <summary>
    /// Used to display the details of an order item.
    /// </summary>
    public class OrderItemDetails
    {
        /// <summary>
        /// The primary key/ID of the order item.
        /// </summary>
        public int OrderItemId { get; set; }

        /// <summary>
        /// The name of the item.
        /// </summary>
        public string? ItemName { get; set; }

        /// <summary>
        /// The quantity that was ordered.
        /// </summary>
        public byte Quantity { get; set; }

        /// <summary>
        /// The item's price multipled by the quantity.
        /// </summary>
        public decimal ExtendedPrice { get; set; }
    }
}