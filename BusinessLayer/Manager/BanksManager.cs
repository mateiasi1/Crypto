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
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using BusinessLayer.Exceptions;

namespace BusinessLayer
{
    public class BanksManager : BanksRepository
    {
        protected Contexts _context;
        private readonly IMapper _mapper;
        private ListDTO<BankDTO> _list = new ListDTO<BankDTO>();
        private ListDTO<BankAccountDTO> accountList = new ListDTO<BankAccountDTO>();
        private ListDTO<BankAccountTransactionDTO> bankAccountTransaction = new ListDTO<BankAccountTransactionDTO>();
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
            _list.Items = new List<BankDTO>();
            try
            {
                var bankList = _context.Bank;
                foreach (var item in bankList)
                {
                    var items = _mapper.Map<BankDTO>(item);
                    _list.Items.Add(items);
                }
                return _list;
            }
            catch (Exception ex)
            {
                throw new ApplicationExpection.ApplicationException(ex);
            }
           
        }
        public ListDTO<BankDTO> GetBankById(int id)
        {
            _list.Items = new List<BankDTO>();
            var bankList = _context.Bank.Where(i => i.Id == id).FirstOrDefault();
            var items = _mapper.Map<BankDTO>(bankList);
            _list.Items.Add(items);

            return _list;
        }

        public ListDTO<BankDTO> AddBank(Bank bank)
        {
            var currency = _context.Currency.Where(c => c.CurrencyAbbreviation == bank.CurrencyAbbreviation).FirstOrDefault();
            bank.CurrencyName = currency.CurrencyName;
            bank.IBAN = GetIban();
            _context.Bank.Add(bank);
            _context.SaveChanges();

            var bankList = _context.Bank;
            foreach (var item in bankList)
            {
                var items = _mapper.Map<BankDTO>(bankList);
            }
            return _list;
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
            return _list;
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
            
            foreach (var item in bankList)
            {
                var items = _mapper.Map<BankAccountDTO>(item);
                accountList.Items.Add(items);
            }

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

            var bankAccountList = _context.BankAccount.Where(b => b.IdUser == bankAccount.IdUser).ToList();
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
            var bankList = from s in _context.BankAccountTransaction orderby s.Date descending select s;
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
            var bankTransactionList = from bankAccount in _context.BankAccountTransaction
                                      join bank in _context.BankAccount on bankAccount.IdBankAccount equals bank.Id
                                      where bank.IdUser == id
                                      select new
                                      {
                                          bankAccount
                                      };
            foreach (var item in bankTransactionList)
            {
                var items = _mapper.Map<BankAccountTransactionDTO>(item.bankAccount);
                bankAccountTransaction.Items.Add(items);
            }
           
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
        public string GetIban()
        {
            ChromeOptions options = new ChromeOptions();
            options.AddArgument("--headless");
            IWebDriver driver = new ChromeDriver(@"D:\DidacticalProjects\Crypto\backend", options);

            driver.Navigate().GoToUrl(@"http://randomiban.com/?country=Romania");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);
            string iban = driver.FindElement(By.Id("demo")).Text;

            return iban;
        }
        #endregion
    }
}
