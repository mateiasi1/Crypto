using BusinessLayer.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.DTO
{
   public class CryptoDTO : BaseDTO
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string CryptoCurrencyName { get; set; }
        public string CryptoCurrencyAbbreviation { get; set; }
        public string Refference { get; set; }
    }
}
