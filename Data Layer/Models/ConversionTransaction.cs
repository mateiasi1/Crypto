using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication17.Models
{
    public class ConversionTransaction
    {
        public int Id { get; set; }
        public int IdConversion { get; set; }
        public double Ammount { get; set; }
        public int IdBankAccount { get; set; }
        public int IdFee { get; set; }
    }
}
