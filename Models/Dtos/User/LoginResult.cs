using System;
using System.Collections.Generic;
using System.Text;

namespace Models.Dtos.User
{
    public class LoginResultDto
    {
        public string Username { get; set; }
        public string Token { get; set; }
    }
}
