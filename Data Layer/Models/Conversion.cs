using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication17.Models
{
    public class Conversion
    {
        public int Id { get; set; }
        public double AmmountToExchange { get; set; }
        public int IdWalletFrom { get; set; }
        public int IdWalletTo { get; set; }
        public string Percentage { get; set; }
    }
}
