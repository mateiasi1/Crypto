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
        ListDTO<CryptoAccountDTO> AddToCryptoAccount(int id, double amount);
        ListDTO<CryptoAccountDTO> WithdrawFromCryptoAccount(int id, double amount);
        #endregion

        #region Crypto Account Transactions
        List<CryptoAccountTransaction> GetAllCryptoTransactions();
        CryptoAccountTransaction GetCryptoTransactionById(int id);
        ListDTO<ConversionTransactionDTO> AddCryptoTransaction(string From, string To, double amount,string type);
        #endregion
    }
}
