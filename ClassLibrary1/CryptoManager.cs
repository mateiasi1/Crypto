using AutoMapper;
using iRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebApplication17.Data;
using WebApplication17.DTO;
using WebApplication17.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace BusinessLayer
{
    public class CryptoManager : ICrypto
    {
        protected Contexts _context;
        CryptoAccountTransaction cryptoAccountTransaction = new CryptoAccountTransaction();
        public CryptoManager(Contexts context)
        {
            _context = context;
        }

        #region Crypto
        public List<Crypto> GetAllCrypto()
        {
            return _context.Crypto.ToList();
        }

        public Crypto GetCryptoById(int id)
        {
            return _context.Crypto.Find(id);
        }

        public Crypto AddCrypto(Crypto crypto)
        {
            var cryptoCurrency = _context.CryptoCurrency.Where(c => c.CryptoCurrencyName == crypto.CryptoCurrencyName).FirstOrDefault();
            crypto.CryptoCurrencyAbbreviation = cryptoCurrency.CryptoCurrencyAbbreviation;
            _context.Crypto.Add(crypto);
            _context.SaveChanges();

            return crypto;
        }

        public Crypto DeleteCrypto(int id)
        {
            var crypto = _context.Crypto.Where(c => c.Id == id).FirstOrDefault();
            _context.Crypto.Remove(crypto);
            _context.SaveChanges();
            return crypto;
        }
        #endregion

        #region Crypto Account
        public List<CryptoAccount> GetAllCryptoAccounts()
        {
            return _context.CryptoAccount.ToList();
        }
        public CryptoAccount GetCryptoAccountById(int id)
        {
            return _context.CryptoAccount.Find(id);
            //    var bank = _context.Bank.Where(c => c.Id == id).FirstOrDefault();
            //return bank;
        }

        public CryptoAccount AddCryptoAccount(CryptoAccount cryptoAccount)
        {
            var crypto = _context.Crypto.Find(cryptoAccount.IdCrypto);
            var cryptoCurrency = _context.CryptoCurrency.Where(b => b.CryptoCurrencyName == cryptoAccount.CryptoCurrencyName).FirstOrDefault();

            cryptoAccount.IdCrypto = crypto.Id;
            cryptoAccount.IdCryptoCurrency = crypto.Id;
            cryptoAccount.Sold = 0;
            _context.CryptoAccount.Add(cryptoAccount);
            _context.SaveChanges();

            return cryptoAccount;

        }

        public CryptoAccount DeleteCryptoAccount(int id)
        {
            var cryptoAccount = _context.CryptoAccount.Find(id);

            _context.CryptoAccount.Remove(cryptoAccount);
            _context.SaveChangesAsync();

            return cryptoAccount;
        }

        public CryptoAccount AddToCryptoAccount(string body)
        {
            JObject fieldData = JsonConvert.DeserializeObject<JObject>(body);
            int id = Convert.ToInt32(fieldData["id"]);
            double amount = Convert.ToDouble(fieldData["amount"]);
            var cryptoAccount = _context.CryptoAccount.Find(id);
            cryptoAccount.Sold += amount;
            //AddCryptoTransaction(cryptoAccount, amount);
            _context.SaveChanges();
            return cryptoAccount;
        }

        public CryptoAccount WithdrawFromCryptoAccount(string body)
        {
            JObject fieldData = JsonConvert.DeserializeObject<JObject>(body);
            int id = Convert.ToInt32(fieldData["id"]);
            double amount = Convert.ToDouble(fieldData["amount"]);

            var cryptoAccount = _context.CryptoAccount.Find(id);
            cryptoAccount.Sold -= amount;

            AddCryptoTransaction(cryptoAccount, amount);
            _context.SaveChanges();
            return cryptoAccount;
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
