using iRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebApplication17.DTO;
using WebApplication17.Models;

namespace BusinessLayer
{
    public class CurrenciesManager : ICurrencies
    {
        protected WebApplication17.Data.Contexts _context;
        public CurrenciesManager(WebApplication17.Data.Contexts context)
        {
            _context = context;
        }
        #region FiatCurrency
        public List<Currency> GetAllCurrencies()
        {
            return _context.Currency.ToList();
        }
        public Currency GetCurrencyById(int id)
        {
            return _context.Currency.Find(id);
        }
        public Currency AddCurrency(Currency currency)
        {
            _context.Currency.Add(currency);
            _context.SaveChanges();
            return currency;
        }
        public Currency DeleteCurrency(int id)
        {
            var currency = _context.Currency.Find(id);

            _context.Currency.Remove(currency);
            _context.SaveChangesAsync();

            return currency;
        }
        #endregion

        #region Crypto Currency
        public List<Crypto> GetAllCryptoCurrencies()
        {
            return _context.Crypto.ToList();
        }
        public Crypto GetCryptoCurrencyById(int id)
        {
            return _context.Crypto.Find(id);
        }
        public Crypto AddCryptoCurrency(Crypto crypto)
        {
            var cryptoCurrency = _context.CryptoCurrency.Where(c => c.CryptoCurrencyName == crypto.CryptoCurrencyName).FirstOrDefault();
            crypto.CryptoCurrencyAbbreviation = cryptoCurrency.CryptoCurrencyAbbreviation;
            _context.Crypto.Add(crypto);
            _context.SaveChanges();

            return crypto;
        }
        public Crypto DeleteCryptoCurrency(int id)
        {
            var cryptoCurrency = _context.Crypto.Find(id);

            _context.Crypto.Remove(cryptoCurrency);
            _context.SaveChangesAsync();

            return cryptoCurrency;
        }

        #endregion
    }
}
