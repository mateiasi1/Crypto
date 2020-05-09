using BusinessLayer.DTO;
using DataLayer.DTO;
using System.Collections.Generic;
using WebApplication17.Models;

namespace iRepository
{
    public interface ICrypto
    {
        #region Crypto
        ListDTO<CryptoDTO> GetAllCrypto();
        ListDTO<CryptoDTO> GetCryptoById(int id);
        ListDTO<CryptoDTO> AddCrypto(Crypto crypto);
        ListDTO<CryptoDTO> DeleteCrypto(int id);
        #endregion

        #region Crypto Account
        ListDTO<CryptoAccountDTO> GetAllCryptoAccounts();
        ListDTO<CryptoAccountDTO> GetCryptoAccountById(int id);
        ListDTO<CryptoAccountDTO> AddCryptoAccount(CryptoAccount cryptoAccount);
        ListDTO<CryptoAccountDTO> DeleteCryptoAccount(int id);
        ListDTO<CryptoAccountDTO> AddToCryptoAccount(string body);
        ListDTO<CryptoAccountDTO> WithdrawFromCryptoAccount(string body);
        #endregion

        #region Crypto Account Transactions
        List<CryptoAccountTransaction> GetAllCryptoTransactions();
        CryptoAccountTransaction GetCryptoTransactionById(int id);
        CryptoAccountTransaction AddCryptoTransaction(CryptoAccount cryptoAccount, double amount);
        #endregion
    }
}
