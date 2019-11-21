using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication17.Models
{
    public class CryptoAccount
    {

        public int Id { get; set; }
        public string CryptoName { get; set; }
        public string Refference { get; set; }
        public double Sold { get; set; }
    }
}
