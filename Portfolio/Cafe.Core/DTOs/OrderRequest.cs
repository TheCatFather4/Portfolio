using System.ComponentModel.DataAnnotations;

namespace Cafe.Core.DTOs
{
    public class OrderRequest
    {
        [Required(ErrorMessage = "Customer ID is required.")]
        public int CustomerId { get; set; }

        [Required(ErrorMessage = "Payment Type ID is required.")]
        public int PaymentTypeId { get; set; }

        [Range(0.00, 1000.00, ErrorMessage = "Tip amound must be between 0 and 1000.")]
        public decimal Tip { get; set; } = 0.00m;
    }
}
