using System;
using System.Collections.Generic;
using System.Text;
using WebApplication17.Models;

namespace BusinessLayer.Services
{
    public class TransferBase
    {
        public virtual bool DoTransfer(Transfer t) {
            return true;
        }
        public virtual double ApplyFee(BaseFee o, double amount)
        {
            return 0;
        }
    }
}
