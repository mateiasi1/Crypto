using BusinessLayer.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.DTO
{
    public class BankDTO : BaseDTO
    {
        public string BankName { get; set; }
        public string IBAN { get; set; }
        public string CurrencyName { get; set; }
        public string CurrencyAbbreviation { get; set; }
    }
}
