namespace Portfolio.Models.Ordering
{
    public class CartItem
    {
        public int ItemID { get; set; }
        public string ItemName { get; set; }
        public byte Quantity { get; set; }
        public decimal? Price { get; set; }
        public byte? ItemStatusID { get; set; }
    }
}
