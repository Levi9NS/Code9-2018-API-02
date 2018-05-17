using System;
using Code9Insta.API.Infrastructure.Entities;
using Microsoft.AspNetCore.Identity;

namespace Code9Insta.API.Infrastructure.Identity
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public byte[] Salt { get; set; }
        public string RefreshToken { get; set; }
        public Profile Profile { get; set; }
    }

    public class ApplicationRole : IdentityRole<Guid>
    {
    }
}