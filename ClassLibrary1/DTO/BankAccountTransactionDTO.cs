using System;
using System.Collections.Generic;
using System.Text;

namespace WebApplication17.DTO
{
    class BankAccountTransactionDTO
    {
        public int Id { get; set; }
        public int IdBankAccount { get; set; }
        public double Ammount { get; set; }
        public string Status { get; set; }
        public int IdFlatRateFee { get; set; }
    }
}
