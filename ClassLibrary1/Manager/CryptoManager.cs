﻿using AutoMapper;
using iRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using WebApplication17.Data;
using WebApplication17.Models;
using BusinessLayer.DTO;
using DataLayer.DTO;

namespace BusinessLayer
{
    public class CryptoManager : ICrypto
    {
        protected Contexts _context;
        private readonly IMapper _mapper;
        CryptoAccountTransaction cryptoAccountTransaction = new CryptoAccountTransaction();
        public ListDTO<CryptoDTO> list = new ListDTO<CryptoDTO>();
        public ListDTO<CryptoAccountDTO> listAccounts = new ListDTO<CryptoAccountDTO>();


        public CryptoManager(Contexts context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        #region Crypto
        public ListDTO<CryptoDTO> GetAllCrypto()
        {
            list.Items = new List<CryptoDTO>();
            var bankList = _context.Crypto.ToList();
            foreach (var item in bankList)
            {
                var items = _mapper.Map<CryptoDTO>(item);
                list.Items.Add(items);
            }
            return list;
        }

        public ListDTO<CryptoDTO> GetCryptoById(int id)
        {
            list.Items = new List<CryptoDTO>();
            var cryptoList = _context.Crypto.Find(id);
            var items = _mapper.Map<CryptoDTO>(cryptoList);
            list.Items.Add(items);

            return list;
        }

        public ListDTO<CryptoDTO> AddCrypto(Crypto crypto)
        {
            list.Items = new List<CryptoDTO>();
            var cryptoCurrency = _context.CryptoCurrency.Where(c => c.CryptoCurrencyName == crypto.CryptoCurrencyName).FirstOrDefault();
            crypto.CryptoCurrencyAbbreviation = cryptoCurrency.CryptoCurrencyAbbreviation;
            _context.Crypto.Add(crypto);
            _context.SaveChanges();

            var cryptoList = _context.Crypto;
            foreach (var item in cryptoList)
            {
                var items = _mapper.Map<CryptoDTO>(cryptoList);
            }
            return list;
        }

        public ListDTO<CryptoDTO> DeleteCrypto(int id)
        {
            list.Items = new List<CryptoDTO>();
            var crypto = _context.Crypto.Where(c => c.Id == id).FirstOrDefault();
            _context.Crypto.Remove(crypto);
            _context.SaveChanges();

            var cryptoList = _context.Crypto;
            foreach (var item in cryptoList)
            {
                var items = _mapper.Map<CryptoDTO>(cryptoList);
            }
            return list;
        }
        #endregion

        #region Crypto Account
        public ListDTO<CryptoAccountDTO> GetAllCryptoAccounts()
        {
            listAccounts.Items = new List<CryptoAccountDTO>();
            var cryptoAccount = _context.CryptoAccount.ToList();
            foreach (var item in cryptoAccount)
            {
                var items = _mapper.Map<CryptoAccountDTO>(item);
                listAccounts.Items.Add(items);
            }
            return listAccounts;
        }
        public ListDTO<CryptoAccountDTO> GetCryptoAccountById(int id)
        {
            listAccounts.Items = new List<CryptoAccountDTO>();
            var cryptoAccount = _context.CryptoAccount.Find(id);

            var items = _mapper.Map<CryptoAccountDTO>(cryptoAccount);
            listAccounts.Items.Add(items);

            return listAccounts;
        }

        public ListDTO<CryptoAccountDTO> AddCryptoAccount(CryptoAccount cryptoAccount)
        {
            var crypto = _context.Crypto.Find(cryptoAccount.IdCrypto);
            var cryptoCurrency = _context.CryptoCurrency.Where(b => b.CryptoCurrencyName == cryptoAccount.CryptoCurrencyName).FirstOrDefault();

            cryptoAccount.IdCrypto = crypto.Id;
            cryptoAccount.IdCryptoCurrency = crypto.Id;
            cryptoAccount.Sold = 0;
            _context.CryptoAccount.Add(cryptoAccount);
            _context.SaveChanges();

            listAccounts.Items = new List<CryptoAccountDTO>();
            var cryptoAccountReturn = _context.CryptoAccount.ToList();
            foreach (var item in cryptoAccountReturn)
            {
                var items = _mapper.Map<CryptoAccountDTO>(item);
                listAccounts.Items.Add(items);
            }
            return listAccounts;
        }

        public ListDTO<CryptoAccountDTO> DeleteCryptoAccount(int id)
        {
            var cryptoAccount = _context.CryptoAccount.Find(id);

            _context.CryptoAccount.Remove(cryptoAccount);
            _context.SaveChangesAsync();

            listAccounts.Items = new List<CryptoAccountDTO>();
            var cryptoAccountReturn = _context.CryptoAccount.ToList();
            foreach (var item in cryptoAccountReturn)
            {
                var items = _mapper.Map<CryptoAccountDTO>(item);
                listAccounts.Items.Add(items);
            }
            return listAccounts;
        }

        public ListDTO<CryptoAccountDTO> AddToCryptoAccount(string body)
        {
            JObject fieldData = JsonConvert.DeserializeObject<JObject>(body);
            int id = Convert.ToInt32(fieldData["id"]);
            double amount = Convert.ToDouble(fieldData["amount"]);
            var cryptoAccount = _context.CryptoAccount.Find(id);
            cryptoAccount.Sold += amount;
            //AddCryptoTransaction(cryptoAccount, amount);
            _context.SaveChanges();

            listAccounts.Items = new List<CryptoAccountDTO>();
            var cryptoAccountReturn = _context.CryptoAccount.ToList();
            foreach (var item in cryptoAccountReturn)
            {
                var items = _mapper.Map<CryptoAccountDTO>(item);
                listAccounts.Items.Add(items);
            }
            return listAccounts;
        }

        public ListDTO<CryptoAccountDTO> WithdrawFromCryptoAccount(string body)
        {
            JObject fieldData = JsonConvert.DeserializeObject<JObject>(body);
            int id = Convert.ToInt32(fieldData["id"]);
            double amount = Convert.ToDouble(fieldData["amount"]);

            var cryptoAccount = _context.CryptoAccount.Find(id);
            cryptoAccount.Sold -= amount;

            AddCryptoTransaction(cryptoAccount, amount);
            _context.SaveChanges();

            listAccounts.Items = new List<CryptoAccountDTO>();
            var cryptoAccountReturn = _context.CryptoAccount.ToList();
            foreach (var item in cryptoAccountReturn)
            {
                var items = _mapper.Map<CryptoAccountDTO>(item);
                listAccounts.Items.Add(items);
            }
            return listAccounts;
        }
        #endregion

        #region Crypto Account Transactions
        public List<CryptoAccountTransaction> GetAllCryptoTransactions()
        {
            return _context.CryptoAccountTransaction.ToList();
        }

        public CryptoAccountTransaction GetCryptoTransactionById(int id)
        {
            return _context.CryptoAccountTransaction.Find(id);
        }

        public CryptoAccountTransaction AddCryptoTransaction(CryptoAccount crypto, double amount)
        {
            var cryptoAccount = _context.CryptoAccount.Find(crypto.Id);
            cryptoAccount.Sold = amount;
            cryptoAccount.IdCrypto = crypto.Id;
            cryptoAccount.IdCryptoCurrency = 0;
            _context.CryptoAccountTransaction.Add(cryptoAccountTransaction);
            _context.SaveChanges();
            return cryptoAccountTransaction;
        }
        #endregion
    }
}