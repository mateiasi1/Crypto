using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Models
{
    public class ChangePassword
    {
        public int UserId { get; set; }
        public string Password { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
