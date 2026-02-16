using Cafe.Core.DTOs.Responses;

namespace Portfolio.Models.Cafe.ShoppingCart
{
    /// <summary>
    /// Used to display the items in a user's shopping cart.
    /// </summary>
    public class ShoppingCart
    {
        /// <summary>
        /// The customer's ID that associates them to their shopping bag.
        /// </summary>
        public int? CustomerID { get; set; }

        /// <summary>
        /// The primary key/ID of the customer's shopping bag.
        /// </summary>
        public int? ShoppingBagID { get; set; }

        /// <summary>
        /// A list of items associated with the customer's shopping bag.
        /// </summary>
        public List<ShoppingBagItemResponse>? Items { get; set; }

        /// <summary>
        /// The total price of all items in their shopping cart.
        /// </summary>
        public decimal Total { get; set; }
    }
}