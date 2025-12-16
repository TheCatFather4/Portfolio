using System.ComponentModel.DataAnnotations;

namespace Cafe.Core.DTOs
{
    /// <summary>
    /// Used for handling requests and mapping data concerning new Payment entities.
    /// </summary>
    public class PaymentRequest
    {
        [Required(ErrorMessage = "An Order ID is required.")]
        public int OrderID { get; set; }

        [Required(ErrorMessage = "A Payment Type ID is required.")]
        public int PaymentTypeID { get; set; }

        [Required(ErrorMessage = "An amount is required.")]
        public decimal Amount { get; set; }
    }
}