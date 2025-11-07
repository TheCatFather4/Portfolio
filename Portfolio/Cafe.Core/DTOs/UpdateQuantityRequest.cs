using System.ComponentModel.DataAnnotations;

namespace Cafe.Core.DTOs
{
    public class UpdateQuantityRequest
    {
        [Required(ErrorMessage = "A quantity is required.")]
        [Range(1, 9, ErrorMessage = "Quantity must be between 1 and 9.")]
        public byte Quantity { get; set; }
    }
}