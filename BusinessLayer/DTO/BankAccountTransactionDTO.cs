using BusinessLayer.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.DTO
{
   public class BankAccountTransactionDTO : BaseDTO
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
