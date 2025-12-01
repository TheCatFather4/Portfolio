namespace Portfolio.Models.Ordering
{
    public class OrderItemDetails
    {
        public int OrderItemId { get; set; }
        public string? ItemName { get; set; }
        public byte Quantity { get; set; }
        public decimal ExtendedPrice { get; set; }
    }
}