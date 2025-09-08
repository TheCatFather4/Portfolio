using Cafe.Core.Entities;

namespace Portfolio.Models
{
    public class CustomerProfile
    {
        public int? CustomerID { get; set; }
        public string FirstName { get; set; }
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
