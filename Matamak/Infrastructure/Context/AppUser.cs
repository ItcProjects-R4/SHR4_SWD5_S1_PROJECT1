using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Context
{
    public class AppUser:IdentityUser
    {
        public string FullName { get; set; }
        public string Address { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }
        public bool IsValid { get; set; }
        public int? ActiveCode { get; set; }
        public DateTime? CodeExpiratioTime { get; set; }
    }
}
