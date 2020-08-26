using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class Transfer
    {
        public int Id { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public double Amount { get; set; }
    }
}
