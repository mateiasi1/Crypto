using iRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebApplication17.DTO;
using WebApplication17.Models;

namespace BusinessLayer
{
    public class CurrenciesManager : iCurrencies
    {
        protected WebApplication17.Data.Contexts _context;
        public CurrenciesManager(WebApplication17.Data.Contexts context)
        {
            _context = context;
        }

        public List<Currency> GetAllCurrencies()
        {
            return _context.Currency.ToList();
        }

        public Currency AddCurrency(Currency currency)
        {
            _context.Currency.Add(currency);
            _context.SaveChanges();
            return currency;
        }
    }
}
