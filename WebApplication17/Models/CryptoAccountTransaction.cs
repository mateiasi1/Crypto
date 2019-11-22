using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication17.Models
{
    public class CryptoAccountTransaction
    {
        public int Id { get; set; }
        public int IdCryptoAccount { get; set; }
        public double Ammount { get; set; }
        public string Status { get; set; }
        public int IdFee { get; set; }
    }
}
