using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication17.Models
{
    public class Token
    {
        public int Id { get; set; }
       public int UserId { get; set; }
       public string TokenValue { get; set; }
        public string Role { get; set; }
       public DateTime StartDate { get; set; }
       public DateTime EndDate { get; set; }
    }
}
