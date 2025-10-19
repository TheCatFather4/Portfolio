using System.ComponentModel.DataAnnotations;

namespace Cafe.Core.DTOs
{
    public class RegisterRequest
    {
        [Required(ErrorMessage = "An email address is required.")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "A password is required.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "A first name is required.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "A last name is required.")]
        public string LastName { get; set; }
    }
}
