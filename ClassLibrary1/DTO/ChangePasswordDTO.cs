using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.DTO
{
    public class ChangePasswordDTO : BaseDTO
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        public string Password { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
