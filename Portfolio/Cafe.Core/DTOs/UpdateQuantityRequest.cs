using System.ComponentModel.DataAnnotations;

namespace Cafe.Core.DTOs
{
    public class UpdateQuantityRequest
    {
        [Required(ErrorMessage = "A quantity is required.")]
        public byte Quantity { get; set; }
    }
}