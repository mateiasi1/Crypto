using System;
using System.Collections.Generic;
using System.Text;

namespace WebApplication17.DTO
{
    public class BankDTO
    {
        public int Id { get; set; }
        public string BankName { get; set; }
        public string IBAN { get; set; }
        public string CurrencyName { get; set; }
        public string CurrencyAbbreviation { get; set; }
    }
}
