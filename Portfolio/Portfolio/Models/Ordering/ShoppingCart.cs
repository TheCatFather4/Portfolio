using Cafe.Core.DTOs.Responses;

namespace Portfolio.Models.Ordering
{
    public class ShoppingCart
    {
        public int? CustomerID { get; set; }
        public int? ShoppingBagID { get; set; }
        public List<ShoppingBagItemResponse>? Items { get; set; }
        public decimal Total { get; set; }
    }
}