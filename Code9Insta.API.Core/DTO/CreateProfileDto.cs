using System;
using System.Collections.Generic;
using System.Text;

namespace Code9Insta.API.Core.DTO
{
    public class CreateProfileDto
    {
        public string Handle { get; set; }
        public bool IsPublic { get; set; }
        public AccountDto User { get; set; }
    }
}
