namespace Cafe.Core.Entities
{
    public class ShoppingBag
    {
        public int ShoppingBagID { get; set; }
        public int CustomerID { get; set; }

        public List<ShoppingBagItem>? Items { get; set; }
    }
}
