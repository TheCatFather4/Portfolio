using System.ComponentModel.DataAnnotations;

namespace Portfolio.Models.Identity
{
    /// <summary>
    /// Model used for logging in a customer.
    /// </summary>
    public class LoginUserForm
    {
        /// <summary>
        /// The user's email associated with their account.
        /// </summary>
        [Required]
        [EmailAddress]
        public string? Email { get; set; }

        /// <summary>
        /// The user's password.
        /// </summary>
        [Required]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        /// <summary>
        /// Used to keep the user logged in. If true, the user will stay logged in.
        /// </summary>
        public bool RememberMe { get; set; }
    }
}