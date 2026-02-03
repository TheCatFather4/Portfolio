using System.ComponentModel.DataAnnotations;

namespace Cafe.Core.DTOs.Requests
{
    /// <summary>
    /// Used for handling PATCH requests concerning an item's quantity.
    /// </summary>
    public class UpdateQuantityRequest
    {
        [Required(ErrorMessage = "A quantity is required.")]
        [Range(1, 9, ErrorMessage = "Quantity must be between 1 and 9.")]
        public byte Quantity { get; set; }
    }
}