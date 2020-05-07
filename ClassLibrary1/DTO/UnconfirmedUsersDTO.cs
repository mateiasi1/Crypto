using BusinessLayer.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataLayer.DTO
{
    public class UnconfirmedUsersDTO : BaseDTO
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Role { get; set; }
        public bool Confirmed { get; set; }
    }
}
