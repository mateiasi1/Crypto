﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication17.Models
{
    public class BankAccount
    {
        public int Id { get; set; }
        public int IdUser { get; set; }
        public int IdCurrency { get; set; }
        public string CurrencyName { get; set; }
        public int IdBank { get; set; }
        public string BankName { get; set; }
        public string IBAN { get; set; }
        public double Sold { get; set; }
    }
}
