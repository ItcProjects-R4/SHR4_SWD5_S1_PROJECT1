using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Infrastructure.DTO
{
    public class RegisterD
    {
        [Required]
        [MinLength(8)]
        public string username { get; set; }
        [Required]
        [MaxLength(100)]
        public string fullName { get; set; }
        [Required]
        public string address { get; set; } 
        [Required]
        [EmailAddress(ErrorMessage = "Invalid Email Format")]
        public string email { get; set; }
        [Required]
        public string password { get; set; }
        [Required]
        [Compare("password",ErrorMessage ="Not Match To Password")]
        public string confirmPassword { get; set; }
    }
}
