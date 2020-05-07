using BusinessLayer.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataLayer.DTO
{
    public class CryptoAccountDTO : BaseDTO
    {

        public int Id { get; set; }
        public string CryptoCurrencyName { get; set; }
        public string CryptoName { get; set; }
        public string Refference { get; set; }
        public double Sold { get; set; }
    }
}
