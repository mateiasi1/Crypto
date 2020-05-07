using System;
using System.Collections.Generic;
using System.Text;
using WebApplication17.Models;

namespace iRepository
{
    public interface ICurrencies
    {
        #region Fiat Currencies
        List<Currency> GetAllCurrencies();
        Currency GetCurrencyById(int id);
        Currency AddCurrency(Currency currency);
        Currency DeleteCurrency(int id);
        #endregion

        #region Crypto Currencies
        List<CryptoCurrency> GetAllCryptoCurrencies();
        CryptoCurrency GetCryptoCurrencyById(int id);
        CryptoCurrency AddCryptoCurrency(CryptoCurrency cryptoCurrency);
        CryptoCurrency DeleteCryptoCurrency(int id);
        #endregion
    }
}
