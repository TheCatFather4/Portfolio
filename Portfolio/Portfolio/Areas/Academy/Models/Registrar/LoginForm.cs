using System.ComponentModel.DataAnnotations;

namespace Portfolio.Areas.Academy.Models.Registrar
{
    /// <summary>
    /// A model used to authenticate users at the 4th Wall Academy.
    /// </summary>
    public class LoginForm
    {
        /// <summary>
        /// The user's email address associated with their account.
        /// </summary>
        [Required]
        [EmailAddress]
        public string? Email { get; set; }

        /// <summary>
        /// The user's password associated with their account.
        /// </summary>
        [Required]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        /// <summary>
        /// Used to keep the user logged in if desired.
        /// </summary>
        public bool RememberMe { get; set; }
    }
}