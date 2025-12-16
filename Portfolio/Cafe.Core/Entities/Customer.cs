namespace Cafe.Core.Entities
{
    public class Customer
    {
        public int CustomerID { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Id { get; set; }
        public int? ShoppingBagID { get; set; }
    }
}