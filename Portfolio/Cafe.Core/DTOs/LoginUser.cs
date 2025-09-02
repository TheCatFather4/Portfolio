using System.ComponentModel.DataAnnotations;

namespace Cafe.Core.DTOs
{
    public class LoginUser
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
