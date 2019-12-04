using System;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication17.Models
{
    public class Bank
    {
        public int Id { get; set; }
        public string BankName { get; set; }
        public string IBAN { get; set; }
        public string CurrencyName { get; set; }
        public string CurrencyAbbreviation { get; set; }
    }
}
