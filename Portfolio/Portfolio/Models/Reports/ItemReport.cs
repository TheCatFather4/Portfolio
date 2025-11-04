namespace Portfolio.Models.Reports
{
    public class ItemReport
    {
        public decimal Price { get; set; }
        public byte Quantity { get; set; }
        public decimal ExtendedPrice { get; set; }
        public DateTime DateSold { get; set; }
    }
}