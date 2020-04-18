﻿using AutoMapper;
using iRepository;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using WebApplication17.DTO;
using WebApplication17.Models;

namespace BusinessLayer
{
    public class CurrenciesManager : ICurrencies
    {
        private readonly IMapper _mapper;
        protected WebApplication17.Data.Contexts _context;
        public CurrenciesManager(WebApplication17.Data.Contexts context, IMapper mapper)
        {
            _mapper = mapper;
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
        public List<CryptoCurrency> GetAllCryptoCurrencies()
        {
            return _context.CryptoCurrency.ToList();
        }
        public CryptoCurrency GetCryptoCurrencyById(int id)
        {
            return _context.CryptoCurrency.Find(id);
        }
        public CryptoCurrency AddCryptoCurrency(CryptoCurrency crypto)
        {
            _context.CryptoCurrency.Add(crypto);
            _context.SaveChanges();

            return crypto;
        }
        public CryptoCurrency DeleteCryptoCurrency(int id)
        {
            var cryptoCurrency = _context.CryptoCurrency.Find(id);

            _context.CryptoCurrency.Remove(cryptoCurrency);
            _context.SaveChangesAsync();

            return cryptoCurrency;
        }

        #endregion
    }
}
