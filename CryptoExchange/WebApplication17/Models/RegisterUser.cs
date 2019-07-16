using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication17.Data.Registration
{
    public class RegisterUser
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string PasswordHash { get; set; }
        public string PasswordSalt { get; set; }
        public string ReferralId { get; set; }
        public bool IsOver18 { get; set; }
        public string PhoneNumber { get; set; }
    }
}
