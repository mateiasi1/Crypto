using iRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebApplication17.Models;

namespace BusinessLayer
{
    public class ConversionsManager : IConversions
    {
        protected WebApplication17.Data.Contexts _context;
        public ConversionsManager(WebApplication17.Data.Contexts context)
        {
            _context = context;
        }

        #region Conversion
        public Conversion AddConversion(Conversion conversion, string percentage)
        {
            conversion.Percentage = percentage;
            _context.SaveChanges();
            return conversion;
        }
        public List<Conversion> GetAllConversions()
        {
            return _context.Conversion.ToList();
        }
        #endregion

        #region Conversion Transactions
        public ConversionTransaction AddConversionTransaction(ConversionTransaction conversionTransaction)
        {
            Conversion conversion = _context.Conversion.FirstOrDefault();
            conversionTransaction.Ammount -= ((Convert.ToDouble(conversion.Percentage) / 100) * conversionTransaction.Ammount);
            _context.ConversionTransaction.Add(conversionTransaction);
            _context.SaveChangesAsync();
            return conversionTransaction;
        }
        public List<ConversionTransaction> GetAllConversionTransactions()
        {
            return _context.ConversionTransaction.ToList();
        }
        #endregion
    }
}
