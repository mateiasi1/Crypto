using System;
using System.Collections.Generic;
using System.Text;

namespace WebApplication17.DTO
{
    public class LoginDTO
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Token { get; set; }
        public string Role { get; set; }
    }
}
