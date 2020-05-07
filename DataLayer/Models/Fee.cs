using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication17.Models
{
    public class Fee
    {
        public int Id { get; set; }
        public double Percentage { get; set; }
        public string UserRole { get; set; }
        public bool Obsolete { get; set; }
    }
}
