using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication17.Models
{
    public class BankAccountTransaction
    {
        public int Id { get; set; }
        public int IdBankAccount { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public double Ammount { get; set; }
        public string Status { get; set; }
        public int IdFlatRateFee { get; set; }
        public string TransactionType { get; set; }
        public DateTime Date { get; set; }
    }
}
