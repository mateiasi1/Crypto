using System;
using System.Collections.Generic;
using System.Text;
using WebApplication17.Models;

namespace iRepository
{
    public interface IConversions
    {
        #region ConversionTransactions
        List<ConversionTransaction> GetAllConversionTransactions();
        ConversionTransaction AddConversionTransaction(ConversionTransaction conversionTransaction);
        #endregion

        #region Conversions
        List<Conversion> GetAllConversions();
        Conversion AddConversion(Conversion conversion, string percentage);
        #endregion
    }
}
