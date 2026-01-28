using System.ComponentModel.DataAnnotations;

namespace Cafe.Core.DTOs.Requests
{
    /// <summary>
    /// Used for handling requests concerning new CafeOrder entities.
    /// </summary>
    public class CafeOrderRequest
    {
        [Required(ErrorMessage = "Customer ID is required.")]
        public int CustomerId { get; set; }

        [Required(ErrorMessage = "Payment Type ID is required.")]
        public int PaymentTypeId { get; set; }

        [Range(0.00, 1000.00, ErrorMessage = "Tip amount must be between 0.00 and 1000.00.")]
        public decimal Tip { get; set; } = 0.00m;
    }
}