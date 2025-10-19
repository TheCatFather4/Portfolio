using System.ComponentModel.DataAnnotations;

namespace Cafe.Core.DTOs
{
    public class AddItemRequest
    {
        [Required(ErrorMessage = "An Shopping Bag ID is required.")]
        public int ShoppingBagId { get; set; }

        [Required(ErrorMessage = "An Item ID is required.")]
        public int ItemId { get; set; }

        [Range(0, 10, ErrorMessage = "Quantity must be between 1 and 10.")]
        public byte Quantity { get; set; }
    }
}