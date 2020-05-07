using BusinessLayer.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.DTO
{
  public  class BankAccountDTO: BaseDTO
    {
        public int Id { get; set; }
        public string CurrencyName { get; set; }
        public string BankName { get; set; }
        public string IBAN { get; set; }
        public double Sold { get; set; }
    }
}
