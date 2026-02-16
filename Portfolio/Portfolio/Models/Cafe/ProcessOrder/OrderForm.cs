using Cafe.Core.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Portfolio.Models.Cafe.ProcessOrder
{
    /// <summary>
    /// Used to create new order entities.
    /// </summary>
    public class OrderForm
    {
        /// <summary>
        /// The primary key/ID of the customer. Used to retrieve the customer's shopping bag total.
        /// </summary>
        public int CustomerId { get; set; }

        /// <summary>
        /// The primary key/ID of the payment type to be used.
        /// </summary>
        public int PaymentTypeId { get; set; }

        /// <summary>
        /// The total of the order before tax and tip.
        /// </summary>
        public decimal SubTotal { get; set; }

        /// <summary>
        /// The tax rate of the order.
        /// </summary>
        public decimal Tax { get; set; }

        /// <summary>
        /// The tip amount of the order.
        /// </summary>
        [Required(ErrorMessage = "This field is required. Enter 0 for no tip.")]
        [DecimalRange("0.00", "1000.00", ErrorMessage = "Tip amount must be between 0.00 and 1000.00.")]
        public decimal? Tip { get; set; }

        /// <summary>
        /// The total price of the order after tax and tip.
        /// </summary>
        public decimal FinalTotal { get; set; }

        /// <summary>
        /// An ID that tells if an order has been paid or is pending payment.
        /// </summary>
        public byte? PaymentStatusId { get; set; }

        /// <summary>
        /// The primary key/ID of the café order.
        /// </summary>
        public int OrderId { get; set; }

        /// <summary>
        /// The total price after tax. Displayed above the tip form.
        /// </summary>
        public decimal TipFormTotal { get; set; }

        /// <summary>
        /// A calculated tip of 10% based on the total price.
        /// </summary>
        public decimal TipTen { get; set; }

        /// <summary>
        /// A calculated tip of 15% based on the total price.
        /// </summary>
        public decimal TipFifteen { get; set; }

        /// <summary>
        /// A calculated tip of 20% based on the total price.
        /// </summary>
        public decimal TipTwenty { get; set; }
    }
}