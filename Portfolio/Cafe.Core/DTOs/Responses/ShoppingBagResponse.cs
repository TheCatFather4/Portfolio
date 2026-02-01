namespace Cafe.Core.DTOs.Responses
{
    /// <summary>
    /// Used for displaying a customer's shopping bag data.
    /// </summary>
    public class ShoppingBagResponse
    {
        public int ShoppingBagID { get; set; }
        public int CustomerID { get; set; }
        public List<ShoppingBagItemResponse>? Items { get; set; }
    }
}