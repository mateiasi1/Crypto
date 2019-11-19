using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication17.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string ReferralId { get; set; }
        public string Role { get; set; }
        public bool IsOver18 { get; set; }
        public bool Confirmed { get; set; }
        public string Token { get; set; }
    }
}
