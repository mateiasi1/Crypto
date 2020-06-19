using AutoMapper;
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
            Crypto cryptoExists = _context.Crypto.Where(c => c.CryptoCurrencyName == crypto.CryptoCurrencyName).FirstOrDefault();
            if(cryptoExists != null)
            {
                list.Items = null;
                return list;
            }
            list.Items = new List<CryptoDTO>();
            var cryptoCurrency = _context.CryptoCurrency.Where(c => c.CryptoCurrencyName == crypto.CryptoCurrencyName).FirstOrDefault();
            crypto.CryptoCurrencyAbbreviation = cryptoCurrency.CryptoCurrencyAbbreviation;

            Random rnd = new Random();
            int referralRandomStart = rnd.Next(10, 99999);
            int referralRandom = rnd.Next(1000000, 9999999);
            string refference = "CRYPTOAPP" + referralRandomStart.ToString() + crypto.CryptoCurrencyName + referralRandom.ToString();
            // crypto.Refference = refference.Replace(" ", "");

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
            var cryptoAccount = _context.CryptoAccount.Where(c => c.IdUser == id).ToList();

            foreach (var item in cryptoAccount)
            {
                var items = _mapper.Map<CryptoAccountDTO>(item);
                listAccounts.Items.Add(items);
            }
            return listAccounts;
        }

        public ListDTO<CryptoAccountDTO> AddCryptoAccount(CryptoAccount cryptoAccount)
        {
            CryptoAccount cryptoAccountExists = _context.CryptoAccount.Where(c => c.CryptoCurrencyName == cryptoAccount.CryptoCurrencyName).FirstOrDefault();
            if (cryptoAccountExists != null)
            {
                listAccounts.Items = null;
                return listAccounts;
            }

            var crypto = _context.Crypto.Find(cryptoAccount.IdCrypto);
            var cryptoCurrency = _context.CryptoCurrency.Where(b => b.CryptoCurrencyName == cryptoAccount.CryptoCurrencyName).FirstOrDefault();

            cryptoAccount.IdCrypto = crypto.Id;
            cryptoAccount.IdCryptoCurrency = crypto.Id;
            cryptoAccount.Sold = 0;

            Random rnd = new Random();
            int referralRandom = rnd.Next(1000000, 9999999);
            cryptoAccount.Refference = "CRYPTOAPP" + cryptoAccount.IdCryptoCurrency.ToString() + cryptoAccount.IdUser + referralRandom.ToString();

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

        public ListDTO<CryptoAccountDTO> AddToCryptoAccount(int id, double amount)
        {
            var cryptoAccount = _context.CryptoAccount.Find(id);
            cryptoAccount.Sold += amount;
            //AddCryptoTransaction(cryptoAccount, amount);
            string type = "Deposit";
            AddCryptoTransaction(cryptoAccount.CryptoCurrencyName, cryptoAccount.CryptoCurrencyName, amount, type);
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

        public ListDTO<CryptoAccountDTO> WithdrawFromCryptoAccount(int id, double amount)
        {

            var cryptoAccount = _context.CryptoAccount.Find(id);
            cryptoAccount.Sold -= amount;
            string type = "Withdraw";
            AddCryptoTransaction(cryptoAccount.CryptoCurrencyName, cryptoAccount.CryptoCurrencyName, amount, type);
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

        public ListDTO<ConversionTransactionDTO> AddCryptoTransaction(string From, string To, double amount, string type)
        {
            ListDTO<ConversionTransactionDTO> conversionAccountTransaction = new ListDTO<ConversionTransactionDTO>();

            var accountTransaction = new ConversionTransaction();
            accountTransaction.From = From;
            accountTransaction.To = To;
            accountTransaction.Ammount = amount;
            accountTransaction.IdFlatRateFee = 0;
            accountTransaction.Status = "Done";
            accountTransaction.Date = DateTime.Now;
            accountTransaction.TransactionType = type;
            _context.ConversionTransaction.Add(accountTransaction);
            _context.SaveChanges();

            conversionAccountTransaction.Items = new List<ConversionTransactionDTO>();
            var conversionTransactions = _context.ConversionTransaction;
            foreach (var item in conversionTransactions)
            {
                var items = _mapper.Map<ConversionTransactionDTO>(item);
                conversionAccountTransaction.Items.Add(items);
            }
            return conversionAccountTransaction;
        }
    #endregion
}
}
