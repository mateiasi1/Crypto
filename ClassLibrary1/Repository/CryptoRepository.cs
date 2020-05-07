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
        Crypto GetCryptoById(int id);
        Crypto AddCrypto(Crypto crypto);
        Crypto DeleteCrypto(int id);
        #endregion

        #region Crypto Account
        ListDTO<CryptoAccountDTO> GetAllCryptoAccounts();
        CryptoAccount GetCryptoAccountById(int id);
        CryptoAccount AddCryptoAccount(CryptoAccount cryptoAccount);
        CryptoAccount DeleteCryptoAccount(int id);
        CryptoAccount AddToCryptoAccount(string body);
        CryptoAccount WithdrawFromCryptoAccount(string body);
        #endregion

        #region Crypto Account Transactions
        List<CryptoAccountTransaction> GetAllCryptoTransactions();
        CryptoAccountTransaction GetCryptoTransactionById(int id);
        CryptoAccountTransaction AddCryptoTransaction(CryptoAccount cryptoAccount, double amount);
        #endregion
    }
}
