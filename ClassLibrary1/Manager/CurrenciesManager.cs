using AutoMapper;
using BusinessLayer.DTO;
using DataLayer.DTO;
using iRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using WebApplication17.Data;
using WebApplication17.Models;

namespace BusinessLayer
{
    public class CurrenciesManager : ICurrencies
    {
        private readonly IMapper _mapper;
        protected Contexts _context;
        public CurrenciesManager(Contexts context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }
        public ListDTO<CurrencyDTO> list = new ListDTO<CurrencyDTO>();
        public ListDTO<CryptoCurrencyDTO> cryptoList = new ListDTO<CryptoCurrencyDTO>();
        #region FiatCurrency
        public ListDTO<CurrencyDTO> GetAllCurrencies()
        {
            list.Items = new List<CurrencyDTO>();
            var currency = _context.Currency.ToList();
            foreach (var item in currency)
            {
                var items = _mapper.Map<CurrencyDTO>(item);
                list.Items.Add(items);
            }
            return list;
        }
        public ListDTO<CurrencyDTO> GetCurrencyById(int id)
        {
            list.Items = new List<CurrencyDTO>();
            var currency = _context.Currency.Find(id);

            var items = _mapper.Map<CurrencyDTO>(currency);
            list.Items.Add(items);

            return list;
        }
        public ListDTO<CurrencyDTO> AddCurrency(Currency currency)
        {
            _context.Currency.Add(currency);
            _context.SaveChanges();

            list.Items = new List<CurrencyDTO>();
            var currencyReturn = _context.Currency.ToList();
            foreach (var item in currencyReturn)
            {
                var items = _mapper.Map<CurrencyDTO>(item);
                list.Items.Add(items);
            }
            return list;
        }
        public ListDTO<CurrencyDTO> DeleteCurrency(int id)
        {
            var currency = _context.Currency.Find(id);

            _context.Currency.Remove(currency);
            _context.SaveChangesAsync();

            list.Items = new List<CurrencyDTO>();
            var currencyReturn = _context.Currency.ToList();
            foreach (var item in currencyReturn)
            {
                var items = _mapper.Map<CurrencyDTO>(item);
                list.Items.Add(items);
            }
            return list;
        }

        #endregion

        #region Crypto Currency
        public ListDTO<CryptoCurrencyDTO> GetAllCryptoCurrencies()
        {
            cryptoList.Items = new List<CryptoCurrencyDTO>();
            var cryptoReturn = _context.CryptoCurrency;
            foreach (var item in cryptoReturn)
            {
                var items = _mapper.Map<CurrencyDTO>(item);
                list.Items.Add(items);
            }
            return cryptoList;
        }
        public ListDTO<CryptoCurrencyDTO> GetCryptoCurrencyById(int id)
        {
            cryptoList.Items = new List<CryptoCurrencyDTO>();
            var cryptoReturn = _context.CryptoCurrency.Find(id);

            var items = _mapper.Map<CurrencyDTO>(cryptoReturn);
            list.Items.Add(items);

            return cryptoList;
        }
        public ListDTO<CryptoCurrencyDTO> AddCryptoCurrency(CryptoCurrency crypto)
        {
            _context.CryptoCurrency.Add(crypto);
            _context.SaveChanges();

            cryptoList.Items = new List<CryptoCurrencyDTO>();
            var cryptoReturn = _context.CryptoCurrency;
            foreach (var item in cryptoReturn)
            {
                var items = _mapper.Map<CurrencyDTO>(item);
                list.Items.Add(items);
            }
            return cryptoList;
        }
        public ListDTO<CryptoCurrencyDTO> DeleteCryptoCurrency(int id)
        {
            var cryptoCurrency = _context.CryptoCurrency.Find(id);

            _context.CryptoCurrency.Remove(cryptoCurrency);
            _context.SaveChangesAsync();

            cryptoList.Items = new List<CryptoCurrencyDTO>();
            var cryptoReturn = _context.CryptoCurrency;
            foreach (var item in cryptoReturn)
            {
                var items = _mapper.Map<CurrencyDTO>(item);
                list.Items.Add(items);
            }
            return cryptoList;
        }

        #endregion
    }
}
