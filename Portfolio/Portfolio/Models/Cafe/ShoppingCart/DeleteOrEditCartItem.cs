using System.ComponentModel.DataAnnotations;

namespace Portfolio.Models.Cafe.ShoppingCart
{
    /// <summary>
    /// Used to update an item's quantity or to delete it from the shopping cart.
    /// </summary>
    public class DeleteOrEditCartItem
    {
        /// <summary>
        /// The customer's ID associated with their shopping bag.
        /// </summary>
        public int CustomerID { get; set; }

        /// <summary>
        /// The primary key/ID of the customer's shopping bag.
        /// </summary>
        public int ShoppingBagItemID { get; set; }


        /// <summary>
        /// The quantity to update.
        /// </summary>
        [Required(ErrorMessage = "A quantity is required")]
        [Range(1, 9, ErrorMessage = "Quantity must be between 1 and 9.")]
        public byte? Quantity { get; set; }

        /// <summary>
        /// The name of the item.
        /// </summary>
        public string ItemName { get; set; }

        /// <summary>
        /// A string representing a path to the item's image.
        /// </summary>
        public string? ItemImgPath { get; set; }

        /// <summary>
        /// The price of the item.
        /// </summary>
        public decimal Price { get; set; }
    }
}