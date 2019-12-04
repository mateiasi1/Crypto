using System;
using System.Collections.Generic;
using System.Text;

namespace WebApplication17.DTO
{
    public class BankDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string IBAN { get; set; }
        public string Currency { get; set; }
    }
}
