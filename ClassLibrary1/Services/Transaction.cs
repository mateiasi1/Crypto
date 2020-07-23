using System;
using System.Collections.Generic;
using System.Text;
using WebApplication17.Data;
using WebApplication17.Models;

namespace BusinessLayer.Services
{
    public class Transaction : TransactionsBase
    {
        protected Contexts _context;
        public Transaction(Contexts context)
        {
            _context = context;
        }
        public override double ApplyFee(BaseFee o, double amount)
        {
            if(o is Fee)
            {
                FeesManager feeCrypto = new FeesManager(_context);
              var currentFeeCrypto = Convert.ToDouble((int)Math.Round((double)(Convert.ToDouble(feeCrypto.GetAllFees()) / 100) * amount));
                return currentFeeCrypto;
            }
            else if(o is FlatRateFee)
            {
                FeesManager flatRateFee = new FeesManager(_context);
                   var flatRate = flatRateFee.GetAllFlatRateFees();
                return flatRate;
            }
            return 0;
        }
    }
}
