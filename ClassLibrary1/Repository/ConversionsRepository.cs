using BusinessLayer.DTO;
using DataLayer.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using WebApplication17.Models;

namespace iRepository
{
    public interface IConversions
    {
        #region ConversionTransactions
        ListDTO<ConversionTransactionDTO> GetAllConversionTransactions();
        ConversionTransaction AddConversionTransaction(ConversionTransaction conversionTransaction);
        #endregion

        #region Conversions
        List<Conversion> GetAllConversions();
        Conversion AddConversion(Conversion conversion, string percentage);
        #endregion

        string FiatExchange(string body);
        string CryptoExchange(string body);
    }
}
