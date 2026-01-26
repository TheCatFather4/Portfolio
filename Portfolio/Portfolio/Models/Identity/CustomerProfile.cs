using Cafe.Core.Entities;
using System.ComponentModel.DataAnnotations;

namespace Portfolio.Models.Identity
{
    /// <summary>
    /// Model used to update a customer's first and last name.
    /// The other properties are used for retrieving and displaying data associated with the customer's account.
    /// </summary>
    public class CustomerProfile
    {
        /// <summary>
        /// The primary key/ID associated to the customer.
        /// </summary>
        public int? CustomerID { get; set; }

        /// <summary>
        /// The first name of the customer.
        /// </summary>
        [Required(ErrorMessage = "A first name is required.")]
        public string? FirstName { get; set; }

        /// <summary>
        /// The last name of the customer.
        /// </summary>
        [Required(ErrorMessage = "A last name is required.")]
        public string? LastName { get; set; }

        /// <summary>
        /// The customer's email address associated with the account.
        /// </summary>
        public string? Email { get; set; }

        /// <summary>
        /// The customer's Identity Id used for authentication and authorization roles.
        /// </summary>
        public string? Id { get; set; }

        /// <summary>
        /// The primary key/ID for a customer's shopping bag.
        /// </summary>
        public int? ShoppingBagId { get; set; }

        /// <summary>
        /// Maps a CustomerProfile object to a new Customer entity object.
        /// </summary>
        /// <returns></returns>
        public Customer ToEntity()
        {
            return new Customer
            {
                CustomerID = CustomerID ?? 0,
                FirstName = FirstName,
                LastName = LastName,
                Email = Email,
                Id = Id,
                ShoppingBagID = ShoppingBagId
            };
        }
    }
}