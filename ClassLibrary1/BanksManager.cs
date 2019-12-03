using AutoMapper;
using iRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebApplication17.Data;
using WebApplication17.DTO;
using WebApplication17.Models;

namespace BusinessLayer
{
    public class BanksManager : IBanks
    {
        protected WebApplication17.Data.Contexts _context;

        public BanksManager(WebApplication17.Data.Contexts context)
        {
            _context = context;
        }

        public List<Bank> GetAllBanks()
        {
            return _context.Bank.ToList();
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

        
}
}
