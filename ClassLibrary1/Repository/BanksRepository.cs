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
        BankDTO GetBankById(int id);
        ListDTO<BankDTO> AddBank(Bank bank);
        ListDTO<BankDTO> DeleteBank(int id);
        #endregion

        #region Bank Account
        ListDTO<BankAccountDTO> GetAllBankAccounts();
        BankAccountDTO GetBankAccountById(int id);
        ListDTO<BankAccountDTO> AddBankAccount(BankAccount bankAccount);
        ListDTO<BankAccountDTO> DeleteBankAccount(int id);
        ListDTO<BankAccountDTO> AddToBankAccount(string body);
        ListDTO<BankAccountDTO> WithdrawFromBankAccount(string body);
        #endregion

        #region Bank Account Transactions
        List<BankAccountTransaction> GetAllTransactions();
        BankAccountTransaction GetTransactionById(int id);
        BankAccountTransaction AddTransaction(BankAccount bankAccount, double amount);
        #endregion
    }
}
