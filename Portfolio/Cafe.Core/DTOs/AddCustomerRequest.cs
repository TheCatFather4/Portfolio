using System.ComponentModel.DataAnnotations;

namespace Cafe.Core.DTOs
{
    /// <summary>
    /// Used to transfer data concerning the registration of new customers. 
    /// </summary>
    public class AddCustomerRequest
    {
        /// <summary>
        /// The customer's first name.
        /// </summary>
        [Required(ErrorMessage = "A first name is required.")]
        public string? FirstName { get; set; }

        /// <summary>
        /// The customer's last name.
        /// </summary>
        [Required(ErrorMessage = "A last name is required.")]
        public string? LastName { get; set; }

        /// <summary>
        /// The customer's email address that they want to associate with their account.
        /// </summary>
        [Required(ErrorMessage = "An email address is required.")]
        [EmailAddress]
        public string? Email { get; set; }

        /// <summary>
        /// The desired password.
        /// </summary>
        [Required(ErrorMessage = "A password is required.")]
        public string? Password { get; set; }

        /// <summary>
        /// The customer's Identity ID (used by ASP.NET Identity for authentication and authorization).
        /// </summary>
        public string? IdentityId { get; set; }
    }
}