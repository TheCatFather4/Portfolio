namespace Cafe.Core.DTOs.Responses
{
    /// <summary>
    /// Used to display item price data to a client from a request.
    /// </summary>
    public class ItemPriceResponse
    {
        public int ItemPriceID { get; set; }
        public int TimeOfDayID { get; set; }
        public decimal Price { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}