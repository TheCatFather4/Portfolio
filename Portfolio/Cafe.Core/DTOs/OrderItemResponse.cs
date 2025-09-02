namespace Cafe.Core.DTOs
{
    public class OrderItemResponse
    {
        public int OrderItemID { get; set; }
        public int ItemPriceID { get; set; }
        public byte Quantity { get; set; }
        public decimal ExtendedPrice { get; set; }
    }
}
