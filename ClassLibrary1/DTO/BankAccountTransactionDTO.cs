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
        public double Ammount { get; set; }
        public string Status { get; set; }
        public int IdFlatRateFee { get; set; }
    }
}
