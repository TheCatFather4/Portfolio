namespace Cafe.Core.DTOs
{
    public class ItemFilter
    {
        public decimal Price { get; set; }
        public byte Quantity { get; set; }
        public decimal ExtendedPrice { get; set; }
        public DateTime DateSold { get; set; }
    }
}