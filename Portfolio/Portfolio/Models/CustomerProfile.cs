using Cafe.Core.Entities;
using System.ComponentModel.DataAnnotations;

namespace Portfolio.Models
{
    public class CustomerProfile
    {
        public int? CustomerID { get; set; }

        [Required(ErrorMessage = "A first name is required.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "A last name is required.")]
        public string LastName { get; set; }

        public string Email { get; set; }
        public string? Id { get; set; }
        public int? ShoppingBagId { get; set; }

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
