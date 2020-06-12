using BusinessLayer.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.DTO
{
    public class UserDTO : BaseDTO
    {
        public string Username { get; set; }
        public string Role { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
