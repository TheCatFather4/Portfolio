namespace Cafe.Core.DTOs
{
    public class ItemResponse
    {
        public int ItemID { get; set; }
        public int CategoryID { get; set; }
        public string ItemName { get; set; }
        public string ItemDescription { get; set; }
        public List<ItemPriceResponse> Prices { get; set; }
    }
}