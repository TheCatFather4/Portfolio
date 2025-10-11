namespace Cafe.Core.DTOs
{
    public class ItemPriceResponse
    {
        public int ItemPriceID { get; set; }
        public int TimeOfDayID { get; set; }
        public decimal Price { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}