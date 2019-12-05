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
    public class BanksManager : IBanks
    {
        protected Contexts _context;
        BankAccountTransaction bankAccountTransaction = new BankAccountTransaction();
        public BanksManager(Contexts context)
        {
            _context = context;
        }

        #region Bank
        public List<Bank> GetAllBanks()
        {
            return _context.Bank.ToList();
        }
        public Bank GetBankById(int id)
        {
            return _context.Bank.Find(id);
            //    var bank = _context.Bank.Where(c => c.Id == id).FirstOrDefault();
            //return bank;
        }

        public List<Bank> AddBank(Bank bank)
        {
            var currency = _context.Currency.Where(c => c.CurrencyAbbreviation == bank.CurrencyAbbreviation).FirstOrDefault();
            bank.CurrencyName = currency.CurrencyName;
            _context.Bank.Add(bank);
            _context.SaveChanges();

            var bankList = _context.Bank.ToList();
            return bankList;
        }

        public Bank DeleteBank(int id)
        {
            var bank = _context.Bank.Where(c => c.Id == id).FirstOrDefault();
            _context.Bank.Remove(bank);
            _context.SaveChanges();
            return bank;
        }
        #endregion

        #region Bank Account
        public List<BankAccount> GetAllBankAccounts()
        {
            return _context.BankAccount.ToList();
        }
        public BankAccount GetBankAccountById(int id)
        {
            return _context.BankAccount.Find(id);
            //    var bank = _context.Bank.Where(c => c.Id == id).FirstOrDefault();
            //return bank;
        }

        public BankAccount AddBankAccount(BankAccount bankAccount)
        {
            var bank = _context.Bank.Find(bankAccount.IdBank);
            var currency = _context.Currency.Where(b => b.CurrencyName == bankAccount.CurrencyName).FirstOrDefault();

            bankAccount.IdBank = bank.Id;
            bankAccount.IdCurrency = currency.Id;
            bankAccount.Sold = 0;
            _context.BankAccount.Add(bankAccount);
            _context.SaveChanges();
            
            return bankAccount;

        }

        public BankAccount DeleteBankAccount(int id)
        {
            var bankAccount = _context.BankAccount.Find(id);

            _context.BankAccount.Remove(bankAccount);
            _context.SaveChangesAsync();

            return bankAccount;
        }

        public BankAccount AddToBankAccount(string body)
        {
           

            JObject fieldData = JsonConvert.DeserializeObject<JObject>(body);
            int id = Convert.ToInt32(fieldData["id"]);
            double amount = Convert.ToDouble(fieldData["amount"]);
            var bankAccount = _context.BankAccount.Find(id);
            bankAccount.Sold += amount;
            AddTransaction(bankAccount, amount);
            _context.SaveChanges();
            return bankAccount;
        }

        public BankAccount WithdrawFromBankAccount(string body)
        {
            JObject fieldData = JsonConvert.DeserializeObject<JObject>(body);
            int id = Convert.ToInt32(fieldData["id"]);
            double amount = Convert.ToDouble(fieldData["amount"]);

            var bankAccount = _context.BankAccount.Find(id);
            bankAccount.Sold -= amount;

            AddTransaction(bankAccount, amount);
            _context.SaveChanges();
            return bankAccount;
        }
        #endregion

        #region Bank Account Transactions
        public List<BankAccountTransaction> GetAllTransactions()
        {
            return _context.BankAccountTransaction.ToList();
        }

        public BankAccountTransaction GetTransactionById(int id)
        {
            return _context.BankAccountTransaction.Find(id);
        }

        public BankAccountTransaction AddTransaction(BankAccount bank, double amount)
        {
            var bankAccount = _context.BankAccount.Find(bank.Id);
            bankAccountTransaction.Ammount = amount;
            bankAccountTransaction.IdBankAccount = bank.Id;
            bankAccountTransaction.IdFlatRateFee = 0;
            bankAccountTransaction.Status = "Done";
            _context.BankAccountTransaction.Add(bankAccountTransaction);
            _context.SaveChanges();
            return bankAccountTransaction;
        }
        #endregion
    }
}
