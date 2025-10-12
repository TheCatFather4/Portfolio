namespace Cafe.Core.DTOs
{
    public class AddItemRequest
    {
        public int ShoppingBagId { get; set; }
        public int ItemId { get; set; }
        public byte Quantity { get; set; }
    }
}