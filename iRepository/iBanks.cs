using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebApplication17.Models;

namespace iRepository
{
    public interface IBanks
    {
        #region Bank
        List<Bank> GetAllBanks();
        Bank GetBankById(int id);
        List<Bank> AddBank(Bank bank);
        Bank DeleteBank(int id);
        #endregion

        #region Bank Account
        List<BankAccount> GetAllBankAccounts();
        List <BankAccount> GetBankAccountById(int id);
        BankAccount AddBankAccount(BankAccount bankAccount);
        BankAccount DeleteBankAccount(int id);
        BankAccount AddToBankAccount(string body);
        BankAccount WithdrawFromBankAccount(string body);
        #endregion

        #region Bank Account Transactions
        List<BankAccountTransaction> GetAllTransactions();
        BankAccountTransaction GetTransactionById(int id);
        BankAccountTransaction AddTransaction(BankAccount bankAccount, double amount);
        #endregion
    }
}
