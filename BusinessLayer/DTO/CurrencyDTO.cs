using BusinessLayer.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.DTO
{
   public class CurrencyDTO : BaseDTO
    {
        public string CurrencyAbbreviation { get; set; }
        public string CurrencyName { get; set; }
    }
}
