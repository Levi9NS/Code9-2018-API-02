using System;
using System.Collections.Generic;
using System.Text;

namespace Code9Insta.API.Core.DTO
{
    public class GetProfileDto
    {
        public string Handle { get; set; }
        public bool IsPublic { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public Guid UserId { get; set; }
        public Guid ProfileId { get; set; }
    }
}
