using System.ComponentModel.DataAnnotations;

namespace Cafe.Core.DTOs.Requests
{
    /// <summary>
    /// Used for adding items to a customer's shopping bag.
    /// </summary>
    public class AddItemToBagRequest
    {
        [Required(ErrorMessage = "A Shopping Bag ID is required.")]
        public int ShoppingBagId { get; set; }

        [Required(ErrorMessage = "An Item ID is required.")]
        public int ItemId { get; set; }

        public byte? ItemStatusId { get; set; }

        [Range(0, 9, ErrorMessage = "Quantity must be between 1 and 9.")]
        public byte Quantity { get; set; }

        public string? ItemName { get; set; }
        public decimal? Price { get; set; }
        public string? ItemImgPath { get; set; }
    }
}