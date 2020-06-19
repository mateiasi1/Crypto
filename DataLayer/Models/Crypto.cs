using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication17.Models
{
    public class Crypto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string CryptoCurrencyName { get; set; }
        public string CryptoCurrencyAbbreviation { get; set; }
        public string Refference { get; set; }
    }
}
