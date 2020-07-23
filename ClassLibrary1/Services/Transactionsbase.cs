using System;
using System.Collections.Generic;
using System.Text;
using WebApplication17.Data;
using WebApplication17.Models;

namespace BusinessLayer.Services
{
    public class TransactionsBase
    {
        public virtual double ApplyFee(BaseFee o, double amount)
        {
            return 0;
        }
    }
}
