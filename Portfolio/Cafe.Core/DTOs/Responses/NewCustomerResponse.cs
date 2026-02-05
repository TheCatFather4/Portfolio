namespace Cafe.Core.DTOs.Responses
{
    /// <summary>
    /// Used to send the customer ID and shopping bag ID of a newly created Customer record to the client.
    /// </summary>
    public class NewCustomerResponse
    {
        public int CustomerID { get; set; }
        public int ShoppingBagID { get; set; }
    }
}