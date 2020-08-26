using BusinessLayer.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataLayer.DTO
{
    public class CryptoCurrencyDTO : BaseDTO
    {
        public string CryptoCurrencyAbbreviation { get; set; }
        public string CryptoCurrencyName { get; set; }
    }
}
