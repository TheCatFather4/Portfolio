namespace Cafe.Core.DTOs
{
    public class ShoppingBagResponse
    {
        public int ShoppingBagID { get; set; }
        public int CustomerID { get; set; }
        public List<ShoppingBagItemResponse> Items { get; set; }
    }
}