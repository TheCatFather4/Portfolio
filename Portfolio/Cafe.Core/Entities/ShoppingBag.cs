namespace Cafe.Core.Entities
{
    public class ShoppingBag
    {
        public int ShoppingBagID { get; set; }
        public int CustomerID { get; set; }

        // Navigation property for joining ShoppingBagItem table.
        public List<ShoppingBagItem>? Items { get; set; }
    }
}