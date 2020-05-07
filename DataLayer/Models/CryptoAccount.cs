using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication17.Models
{
    public class CryptoAccount
    {
        public int Id { get; set; }
        public int IdUser { get; set; }
        public int IdCryptoCurrency { get; set; }
        public string CryptoCurrencyName { get; set; }
        public int IdCrypto { get; set; }
        public string CryptokName { get; set; }
        public int IdBankAccount { get; set; }
        public string Refference { get; set; }
        public double Sold { get; set; }
    }
}
