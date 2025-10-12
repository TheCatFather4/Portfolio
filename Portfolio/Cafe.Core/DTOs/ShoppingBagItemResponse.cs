namespace Cafe.Core.DTOs
{
    public class ShoppingBagItemResponse
    {
        public int ShoppingBagItemID { get; set; }
        public int ItemID { get; set; }
        public byte Quantity { get; set; }
        public string ItemName { get; set; }
        public decimal Price { get; set; }
    }
}
