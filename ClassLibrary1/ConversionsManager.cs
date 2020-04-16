using iRepository;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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

        public string FiatExchange(string body)
        {
            JObject fieldData = JsonConvert.DeserializeObject<JObject>(body);
            int id = Convert.ToInt32(fieldData["id"]);
            string selectedValueFrom = Convert.ToString(fieldData["selectedValueFrom"]);
            string selectedValueTo = Convert.ToString(fieldData["selectedValueTo"]);
            double amount = Convert.ToDouble(fieldData["amountFrom"]);
            var fiatCurrencyName = _context.Currency.Where(i => i.CurrencyAbbreviation == selectedValueFrom).Select(i => i.CurrencyName).FirstOrDefault();
            var cryptoCurrencyName = _context.CryptoCurrency.Where(i => i.CryptoCurrencyAbbreviation == selectedValueTo).Select(i => i.CryptoCurrencyName).FirstOrDefault();

            var bankAccount = _context.BankAccount.Where(i => i.Id == id && i.CurrencyName == fiatCurrencyName).FirstOrDefault();
            var cryptoAccount = _context.CryptoAccount.Where(i => i.CryptoCurrencyName == cryptoCurrencyName).FirstOrDefault();
            bankAccount.Sold -= amount;
            cryptoAccount.Sold += amount;
            _context.SaveChanges();
            return "ok";
        }
        #endregion
    }
}
