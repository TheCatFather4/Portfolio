namespace Portfolio.Models.Cafe.ProcessOrder
{
    /// <summary>
    /// The data associated with a specific café order.
    /// </summary>
    public class OrderDetails
    {
        /// <summary>
        /// The primary key/ID of the order.
        /// </summary>
        public int OrderId { get; set; }

        /// <summary>
        /// The primary key/ID of the order's payment status.
        /// </summary>
        public byte? PaymentStatusId { get; set; }

        /// <summary>
        /// The date the order was created.
        /// </summary>
        public DateTime OrderDate { get; set; }

        /// <summary>
        /// The subtotal of the items in the order.
        /// </summary>
        public decimal SubTotal { get; set; }

        /// <summary>
        /// The tax rate for the order.
        /// </summary>
        public decimal Tax { get; set; }

        /// <summary>
        /// The amount that the customer tipped.
        /// </summary>
        public decimal Tip { get; set; }

        /// <summary>
        /// The subtotal, tax, and tip added together.
        /// </summary>
        public decimal FinalTotal { get; set; }

        /// <summary>
        /// A list of items associated with the order.
        /// </summary>
        public List<OrderItemDetails>? Items { get; set; }
    }
}