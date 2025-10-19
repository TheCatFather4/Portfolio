﻿using System.ComponentModel.DataAnnotations;

namespace Cafe.Core.DTOs
{
    public class LoginRequest
    {
        [Required(ErrorMessage = "A valid email address is required.")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "A valid password is required.")]
        public string Password { get; set; }
    }
}
