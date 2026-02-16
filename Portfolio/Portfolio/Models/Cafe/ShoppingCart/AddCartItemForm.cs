using System.ComponentModel.DataAnnotations;

namespace Portfolio.Models.Cafe.ShoppingCart
{
    /// <summary>
    /// Used to add item's to a customer's shopping cart.
    /// All properties except the Quantity are automatically populated.
    /// </summary>
    public class AddCartItemForm
    {
        /// <summary>
        /// The item's primary key/ID.
        /// </summary>
        public int ItemID { get; set; }

        /// <summary>
        /// The name of the item.
        /// </summary>
        public string? ItemName { get; set; }

        /// <summary>
        /// A string that represents a path to an image for the item.
        /// </summary>
        public string? ItemImgPath { get; set; }

        /// <summary>
        /// The desired quantity of an item to add to the cart.
        /// </summary>
        [Required(ErrorMessage = "A quantity is required.")]
        [Range(1, 9, ErrorMessage = "Quantity must be between 1 and 9")]
        public byte? Quantity { get; set; }

        /// <summary>
        /// The price of an item.
        /// </summary>
        public decimal? Price { get; set; }

        /// <summary>
        /// An ID that represents whether the item was recently added to the menu or not.
        /// </summary>
        public byte? ItemStatusID { get; set; }
    }
}