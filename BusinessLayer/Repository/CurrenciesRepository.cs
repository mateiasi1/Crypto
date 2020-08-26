using BusinessLayer.DTO;
using DataLayer.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using WebApplication17.Models;

namespace iRepository
{
    public interface ICurrencies
    {
        #region Fiat Currencies
        ListDTO<CurrencyDTO> GetAllCurrencies();
        ListDTO<CurrencyDTO> GetCurrencyById(int id);
        ListDTO<CurrencyDTO> AddCurrency(Currency currency);
        ListDTO<CurrencyDTO> DeleteCurrency(int id);
        #endregion

        #region Crypto Currencies
        ListDTO<CryptoCurrencyDTO> GetAllCryptoCurrencies();
        ListDTO<CryptoCurrencyDTO> GetCryptoCurrencyById(int id);
        ListDTO<CryptoCurrencyDTO> AddCryptoCurrency(CryptoCurrency cryptoCurrency);
        ListDTO<CryptoCurrencyDTO> DeleteCryptoCurrency(int id);
        #endregion
    }
}
