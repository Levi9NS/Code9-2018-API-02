using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Code9Insta.API.Infrastructure.Identity;

namespace Code9Insta.API.Infrastructure.Entities
{
    public class Profile
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public bool IsPublic { get; set; }
        [Required]
        public string Handle { get; set; }

        public ApplicationUser User { get; set; }
    }
}
