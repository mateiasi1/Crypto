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
        List<Crypto> GetAllCryptoCurrencies();
        Crypto GetCryptoCurrencyById(int id);
        Crypto AddCryptoCurrency(Crypto cryptoCurrency);
        Crypto DeleteCryptoCurrency(int id);
        #endregion
    }
}
