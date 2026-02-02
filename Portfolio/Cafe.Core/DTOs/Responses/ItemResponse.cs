namespace Cafe.Core.DTOs.Responses
{
    /// <summary>
    /// Used to display item data to a client from a request.
    /// </summary>
    public class ItemResponse
    {
        public int ItemID { get; set; }
        public int CategoryID { get; set; }
        public string? ItemName { get; set; }
        public string? ItemDescription { get; set; }
        public List<ItemPriceResponse>? Prices { get; set; }
    }
}