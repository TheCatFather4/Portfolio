namespace Cafe.Core.DTOs.Responses
{
    /// <summary>
    /// Used for displaying an individual item in the customer's shopping bag.
    /// </summary>
    public class ShoppingBagItemResponse
    {
        public int ShoppingBagItemID { get; set; }
        public int ShoppingBagID { get; set; }
        public int ItemID { get; set; }
        public byte Quantity { get; set; }
        public string ItemName { get; set; }
        public decimal Price { get; set; }
        public string ItemImgPath { get; set; }
    }
}