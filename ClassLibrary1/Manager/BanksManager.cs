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
        BankAccountTransaction bankAccountTransaction = new BankAccountTransaction();
        public BanksManager(IMapper mapper, Contexts context)
        {
            _mapper = mapper;
            _context = context;
        }
       
        #region Bank
        public ListDTO<BankDTO> GetAllBanks()
        {
            list.Items = new List<BankDTO>();
            var bankList= _context.Bank;
            foreach(var item in bankList)
            {
               var items = _mapper.Map<BankDTO>(item);
                list.Items.Add(items);
            }
            return list;
        }
        public BankDTO GetBankById(int id)
        {
            var bank = _context.Bank.Find(id);
            var item = _mapper.Map<BankDTO>(bank);

            return item;
            //    var bank = _context.Bank.Where(c => c.Id == id).FirstOrDefault();
            //return bank;
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
            var bankList= _context.Bank;
            foreach(var item in bankList)
            {
               var items = _mapper.Map<BankDTO>(bankList);
            }
            return list;
        }
        #endregion

        #region Bank Account
        public ListDTO<BankAccountDTO> GetAllBankAccounts()
        {

            var bankAccountList = _context.BankAccount;
            foreach (var item in bankAccountList)
            {
                var items = _mapper.Map<BankAccountDTO>(item);
            }
            return accountList;
        }

        public BankAccountDTO GetBankAccountById(int id)
        {

            var bank = _context.BankAccount.Where(i => i.IdUser == id).ToList();
            var item = _mapper.Map<BankAccountDTO>(bank);

            return item;
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
                var items = _mapper.Map<BankAccountDTO>(bankAccountList);
           
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
                var items = _mapper.Map<BankAccountDTO>(bankAccountList);
           
            }
            return accountList;
        }

        public ListDTO<BankAccountDTO> AddToBankAccount(string body)
        {
           

            JObject fieldData = JsonConvert.DeserializeObject<JObject>(body);
            int id = Convert.ToInt32(fieldData["id"]);
            double amount = Convert.ToDouble(fieldData["amount"]);
            var bankAccount = _context.BankAccount.Find(id);
            bankAccount.Sold += amount;
            AddTransaction(bankAccount, amount);
            _context.SaveChanges();

            var bankAccountList = _context.BankAccount;
            foreach (var item in bankAccountList)
            {
                var items = _mapper.Map<BankAccountDTO>(bankAccountList);
           
            }
            return accountList;
        }

        public ListDTO<BankAccountDTO> WithdrawFromBankAccount(string body)
        {
            JObject fieldData = JsonConvert.DeserializeObject<JObject>(body);
            int id = Convert.ToInt32(fieldData["id"]);
            double amount = Convert.ToDouble(fieldData["amount"]);

            var bankAccount = _context.BankAccount.Find(id);
            FeesManager flatRateFee = new FeesManager(_context);

            var flatRate = flatRateFee.GetAllFlatRateFees();
            bankAccount.Sold -= (amount + flatRate);
            if (bankAccount.Sold < 0)
            {
                return null;
            }
            AddTransaction(bankAccount, amount);
            _context.SaveChanges();

            var bankAccountList = _context.BankAccount;
            foreach (var item in bankAccountList)
            {
                var items = _mapper.Map<BankAccountDTO>(bankAccountList);
           
            }
            return accountList;
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
