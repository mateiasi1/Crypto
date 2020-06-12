using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.DTO
{
    public class TransferDTO : BaseDTO
    {
        public int Id { get; set; }
        public int IdUserFrom { get; set; }
        public string RefferalUserTo { get; set; }
        public string CoinName { get; set; }
        public double Amount { get; set; }
        public DateTime Date { get; set; }
    }
}
