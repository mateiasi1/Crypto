using iRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebApplication17.Data;
using WebApplication17.Models;

namespace BusinessLayer
{
    public class FeesManager : IFee
    {
        protected Contexts _context;
        public FeesManager(Contexts context)
        {
            _context = context;
        }

        #region Flat rate fee
        public List<FlatRateFee> GetAllFlatRateFees()
        {
            return _context.FlatRateFee.ToList();
        }
        public FlatRateFee AddFlatRateFee(FlatRateFee flatRateFee)
        {
            FlatRateFee flat = _context.FlatRateFee.Where(item => item.Obsolete == false).FirstOrDefault();
            if (flat == null)
            {
                _context.FlatRateFee.Add(flatRateFee);
                _context.SaveChangesAsync();
            }
            else
            {
                flat.Obsolete = true;
                 _context.SaveChangesAsync();

                _context.FlatRateFee.Add(flatRateFee);
                _context.SaveChangesAsync();
            }
            return flatRateFee;
        }
        public FlatRateFee DeleteFlatRateFee(int id)
        {
            var flatRateFee = _context.FlatRateFee.Where(c => c.Id == id).FirstOrDefault();
            _context.FlatRateFee.Remove(flatRateFee);
            _context.SaveChangesAsync();

            return flatRateFee;
        }
        #endregion


        #region Fee
        public List<Fee> GetAllFees()
        {
           return _context.Fee.ToList();
        }

        public Fee AddFee(Fee fee)
        {
            Fee fees = _context.Fee.Where(item => item.Obsolete == false).FirstOrDefault();
            if (fees == null)
            {
                _context.Fee.Add(fee);
                _context.SaveChanges();
            }
            else
            {
                fees.Obsolete = true;
                _context.SaveChanges();

                _context.Fee.Add(fee);
                _context.SaveChanges();
            }
            return fee;
        }
        public Fee DeleteFee(int id)
        {
            var fee =  _context.Fee.Find(id);
            _context.Fee.Remove(fee);
            _context.SaveChangesAsync();
            return fee;
        }

        #endregion
    }
}
