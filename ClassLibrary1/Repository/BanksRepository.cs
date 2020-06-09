using BusinessLayer.DTO;
using System.Collections.Generic;
using DataLayer.DTO;
using WebApplication17.Models;

namespace iRepository
{
    public interface BanksRepository
    {
        #region Bank
        ListDTO<BankDTO> GetAllBanks();
        ListDTO<BankDTO> GetBankById(int id);
        ListDTO<BankDTO> AddBank(Bank bank);
        ListDTO<BankDTO> DeleteBank(int id);
        #endregion

        #region Bank Account
        ListDTO<BankAccountDTO> GetAllBankAccounts();
        ListDTO<BankAccountDTO> GetBankAccountById(int id);
        ListDTO<BankAccountDTO> AddBankAccount(BankAccount bankAccount);
        ListDTO<BankAccountDTO> DeleteBankAccount(int id);
        ListDTO<BankAccountDTO> AddToBankAccount(int id, double amount);
        ListDTO<BankAccountDTO> WithdrawFromBankAccount(int id, double amount);
        #endregion

        #region Bank Account Transactions
        ListDTO<BankAccountTransactionDTO> GetAllTransactions();
        ListDTO<BankAccountTransactionDTO> GetTransactionById(int id);
        ListDTO<BankAccountTransactionDTO> AddTransaction(BankAccount bankAccount, double amount, string type);
        #endregion
    }
}
