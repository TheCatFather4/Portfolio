using System.ComponentModel.DataAnnotations;

namespace Portfolio.Models.Identity
{
    /// <summary>
    /// Model used for registering a new customer with the café.
    /// </summary>
    public class RegisterUserForm
    {
        /// <summary>
        /// Property used to get or set a customer's username.
        /// </summary>
        [Required]
        [EmailAddress]
        public string? Email { get; set; }

        /// <summary>
        /// Property used to get or set a customer's password.
        /// </summary>
        [Required]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        /// <summary>
        /// Property used to get or set a matching password.
        /// </summary>
        [Required(ErrorMessage = "The Confirm Password field is required.")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string? ConfirmPassword { get; set; }
    }
}