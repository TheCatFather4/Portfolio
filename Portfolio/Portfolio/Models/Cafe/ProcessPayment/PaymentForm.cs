using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Portfolio.Models.Cafe.ProcessPayment
{
    /// <summary>
    /// Used to pay open café orders.
    /// </summary>
    public class PaymentForm
    {
        /// <summary>
        /// The primary key/ID of the order to be paid.
        /// </summary>
        public int OrderId { get; set; }

        /// <summary>
        /// A select list of payment types for the user to choose from.
        /// </summary>
        public SelectList? PaymentTypes { get; set; }

        /// <summary>
        /// The selected ID of the payment type chosen from the SelectList.
        /// </summary>
        [Required(ErrorMessage = "You must select a payment method.")]
        public byte? PaymentTypeId { get; set; }

        /// <summary>
        /// The amount the user would like to pay.
        /// </summary>
        [Required(ErrorMessage = "You must enter a payment amount.")]
        public decimal? Amount { get; set; }

        /// <summary>
        /// The date of the payment.
        /// </summary>
        public DateTime TransactionDate { get; set; }

        /// <summary>
        /// An ID that tells if a payment has been made or is pending.
        /// </summary>
        public byte PaymentStatusId { get; set; }

        /// <summary>
        /// The final total to be paid.
        /// </summary>
        public decimal FinalTotal { get; set; }

        /// <summary>
        /// The name of the payment type used.
        /// </summary>
        public string? PaymentTypeName { get; set; }
    }
}