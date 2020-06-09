using AutoMapper;
using iRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using DataLayer.DTO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using BusinessLayer.DTO;
using WebApplication17.Data;
using WebApplication17.Models;

namespace BusinessLayer
{
    public class BanksManager : BanksRepository
    {
        protected Contexts _context;
        private readonly IMapper _mapper;
        public ListDTO<BankDTO> list = new ListDTO<BankDTO>();
        public ListDTO<BankAccountDTO> accountList = new ListDTO<BankAccountDTO>();
        public ListDTO<BankAccountTransactionDTO> bankAccountTransaction = new ListDTO<BankAccountTransactionDTO>();
        public BanksManager(IMapper mapper, Contexts context)
        {
            _mapper = mapper;
            _context = context;
        }

        public BanksManager()
        {
        }

        #region Bank
        public ListDTO<BankDTO> GetAllBanks()
        {
            list.Items = new List<BankDTO>();
            var bankList = _context.Bank;
            foreach (var item in bankList)
            {
                var items = _mapper.Map<BankDTO>(item);
                list.Items.Add(items);
            }
            return list;
        }
        public ListDTO<BankDTO> GetBankById(int id)
        {
            list.Items = new List<BankDTO>();
            var bankList = _context.Bank.Find(id);
            var items = _mapper.Map<BankDTO>(bankList);
            list.Items.Add(items);

            return list;
        }

        public ListDTO<BankDTO> AddBank(Bank bank)
        {
            var currency = _context.Currency.Where(c => c.CurrencyAbbreviation == bank.CurrencyAbbreviation).FirstOrDefault();
            bank.CurrencyName = currency.CurrencyName;
            _context.Bank.Add(bank);
            _context.SaveChanges();

            var bankList = _context.Bank;
            foreach (var item in bankList)
            {
                var items = _mapper.Map<BankDTO>(bankList);
            }
            return list;
        }

        public ListDTO<BankDTO> DeleteBank(int id)
        {
            var bank = _context.Bank.Where(c => c.Id == id).FirstOrDefault();
            _context.Bank.Remove(bank);
            _context.SaveChanges();
            var bankList = _context.Bank;
            foreach (var item in bankList)
            {
                var items = _mapper.Map<BankDTO>(bankList);
            }
            return list;
        }
        #endregion

        #region Bank Account
        public ListDTO<BankAccountDTO> GetAllBankAccounts()
        {
            accountList.Items = new List<BankAccountDTO>();
            var bankAccountList = _context.BankAccount;
            foreach (var item in bankAccountList)
            {
                var items = _mapper.Map<BankAccountDTO>(item);
                accountList.Items.Add(items);
            }
            return accountList;
        }

        public ListDTO<BankAccountDTO> GetBankAccountById(int id)
        {

            accountList.Items = new List<BankAccountDTO>();
            var bankList = _context.BankAccount.Where(i => i.IdUser == id).ToList();
            var items = _mapper.Map<BankAccountDTO>(bankList);
            accountList.Items.Add(items);

            return accountList;
        }

        public ListDTO<BankAccountDTO> AddBankAccount(BankAccount bankAccount)
        {
            var bank = _context.Bank.Find(bankAccount.IdBank);
            var currency = _context.Currency.Where(b => b.CurrencyName == bankAccount.CurrencyName).FirstOrDefault();

            bankAccount.IdBank = bank.Id;
            bankAccount.IdCurrency = currency.Id;
            bankAccount.Sold = 0;
            _context.BankAccount.Add(bankAccount);
            _context.SaveChanges();

            var bankAccountList = _context.BankAccount;
            foreach (var item in bankAccountList)
            {
                var items = _mapper.Map<BankAccountDTO>(item);
                accountList.Items.Add(items);
            }
            return accountList;

        }

        public ListDTO<BankAccountDTO> DeleteBankAccount(int id)
        {
            var bankAccount = _context.BankAccount.Find(id);

            _context.BankAccount.Remove(bankAccount);
            _context.SaveChangesAsync();

            var bankAccountList = _context.BankAccount;
            foreach (var item in bankAccountList)
            {
                var items = _mapper.Map<BankAccountDTO>(item);
                accountList.Items.Add(items);

            }
            return accountList;
        }

        public ListDTO<BankAccountDTO> AddToBankAccount(int id, double amount)
        {
            accountList.Items = new List<BankAccountDTO>();
            var bankAccount = _context.BankAccount.Find(id);
            bankAccount.Sold += amount;
            string type = "Deposit";
            AddTransaction(bankAccount, amount, type);
            _context.SaveChanges();

            var bankAccountList = _context.BankAccount;
            foreach (var item in bankAccountList)
            {
                var items = _mapper.Map<BankAccountDTO>(item);
                accountList.Items.Add(items);

            }
            return accountList;
        }

        public ListDTO<BankAccountDTO> WithdrawFromBankAccount(int id, double amount)
        {
            accountList.Items = new List<BankAccountDTO>();

            var bankAccount = _context.BankAccount.Find(id);
            FeesManager flatRateFee = new FeesManager(_context);

            var flatRate = flatRateFee.GetAllFlatRateFees();
            bankAccount.Sold -= (amount + flatRate);
            if (bankAccount.Sold < 0)
            {
                return null;
            }
            string type = "Withdraw";
            AddTransaction(bankAccount, amount, type);
            _context.SaveChanges();

            var bankAccountList = _context.BankAccount;
            foreach (var item in bankAccountList)
            {
                var items = _mapper.Map<BankAccountDTO>(item);
                accountList.Items.Add(items);
            }
            return accountList;
        }
        #endregion

        #region Bank Account Transactions
        public ListDTO<BankAccountTransactionDTO> GetAllTransactions()
        {
            bankAccountTransaction.Items = new List<BankAccountTransactionDTO>();
            var bankList = _context.BankAccountTransaction;
            foreach (var item in bankList)
            {
                var items = _mapper.Map<BankAccountTransactionDTO>(item);
                bankAccountTransaction.Items.Add(items);
            }
            return bankAccountTransaction;
        }

        public ListDTO<BankAccountTransactionDTO> GetTransactionById(int id)
        {

            bankAccountTransaction.Items = new List<BankAccountTransactionDTO>();
            var bankTransactionList = _context.BankAccountTransaction.Find(id);
            var items = _mapper.Map<BankAccountTransactionDTO>(bankTransactionList);
            bankAccountTransaction.Items.Add(items);

            return bankAccountTransaction;
        }

        public ListDTO<BankAccountTransactionDTO> AddTransaction(BankAccount bank, double amount, string type)
        {
            var accountTransaction = new BankAccountTransaction();
            accountTransaction.From = bank.CurrencyName;
            accountTransaction.To = bank.CurrencyName;
            accountTransaction.Ammount = amount;
            accountTransaction.IdBankAccount = bank.Id;
            accountTransaction.IdFlatRateFee = 0;
            accountTransaction.Status = "Done";
            accountTransaction.Date = DateTime.Now;
            accountTransaction.TransactionType = type;
            _context.BankAccountTransaction.Add(accountTransaction);
            _context.SaveChanges();

            bankAccountTransaction.Items = new List<BankAccountTransactionDTO>();
            var bankList = _context.BankAccountTransaction;
            foreach (var item in bankList)
            {
                var items = _mapper.Map<BankAccountTransactionDTO>(item);
                bankAccountTransaction.Items.Add(items);
            }
            return bankAccountTransaction;
        }
        #endregion
    }
}
