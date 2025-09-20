using Cafe.Core.Entities;

namespace Portfolio.Models.Ordering
{
    public class ShoppingCart
    {
        public int? CustomerID { get; set; }
        public int? ShoppingBagID { get; set; }
        public List<ShoppingBagItem>? Items { get; set; }
        public decimal Total { get; set; }
    }
}
