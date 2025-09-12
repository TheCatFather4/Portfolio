namespace Portfolio.Models.Ordering
{
    public class ItemUpdate
    {
        public int CustomerID { get; set; }
        public int ShoppingBagItemID { get; set; }
        public byte Quantity { get; set; }
        public string ItemName { get; set; }
        public decimal Price { get; set; }
    }
}
