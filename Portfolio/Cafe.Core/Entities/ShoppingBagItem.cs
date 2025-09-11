namespace Cafe.Core.Entities
{
    public class ShoppingBagItem
    {
        public int ShoppingBagItemID { get; set; }
        public int ShoppingBagID { get; set; }
        public int ItemID { get; set; }
        public byte Quantity { get; set; }
        public string? ItemName { get; set; }
        public decimal? Price { get; set; }
        public byte? ItemStatusID { get; set; }
    }
}
