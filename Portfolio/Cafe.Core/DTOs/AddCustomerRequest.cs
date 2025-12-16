using System.ComponentModel.DataAnnotations;

namespace Cafe.Core.DTOs
{
    /// <summary>
    /// Used for handling requests and mapping data concerning new Customer entities.
    /// </summary>
    public class AddCustomerRequest
    {
        [Required(ErrorMessage = "A first name is required.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "A last name is required.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "An email address is required.")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "A password is required.")]
        public string Password { get; set; }

        public string? IdentityId { get; set; }
    }
}