using AutoMapper;
using BusinessLayer.DTO;
using Data_Layer.Models;
using DataLayer.DTO;
using iRepository;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using WebApplication17.Data;
using WebApplication17.Models;

namespace BusinessLayer
{
    public class ConversionsManager : IConversions
    {
        protected Contexts _context;
        private readonly IMapper _mapper;
        public ListDTO<ConversionTransactionDTO> conversionTransactions = new ListDTO<ConversionTransactionDTO>();
        public ConversionsManager(Contexts context, IMapper mapper)
        {
            _mapper = mapper;
               _context = context;
        }

        #region Conversion
        public Conversion AddConversion(Conversion conversion, string percentage)
        {
            conversion.Percentage = percentage;
            _context.SaveChanges();
            return conversion;
        }
        public List<Conversion> GetAllConversions()
        {
            return _context.Conversion.ToList();
        }
        #endregion

        #region Conversion Transactions
        public ConversionTransaction AddConversionTransaction(ConversionTransaction conversionTransaction)
        {
            Conversion conversion = _context.Conversion.FirstOrDefault();
            conversionTransaction.Ammount -= ((Convert.ToDouble(conversion.Percentage) / 100) * conversionTransaction.Ammount);
            _context.ConversionTransaction.Add(conversionTransaction);
            _context.SaveChangesAsync();
            return conversionTransaction;
        }
        public ListDTO<ConversionTransactionDTO> GetAllConversionTransactions()
        {
            conversionTransactions.Items = new List<ConversionTransactionDTO>();
            var bankList = from s in _context.ConversionTransaction orderby s.Date descending select s;
            foreach (var item in bankList)
            {
                var items = _mapper.Map<ConversionTransactionDTO>(item);
                conversionTransactions.Items.Add(items);
            }
            return conversionTransactions;

        }

        public string FiatExchange(string body)
        {
            JObject fieldData = JsonConvert.DeserializeObject<JObject>(body);
            int id = Convert.ToInt32(fieldData["id"]);
            string selectedValueFrom = Convert.ToString(fieldData["selectedValueFrom"]);
            string selectedValueTo = Convert.ToString(fieldData["selectedValueTo"]);
            double amount = Convert.ToDouble(fieldData["amountFrom"]);
            var fiatCurrencyName = _context.Currency.Where(i => i.CurrencyAbbreviation == selectedValueFrom).Select(i => i.CurrencyName).FirstOrDefault();
            var cryptoCurrencyName = _context.CryptoCurrency.Where(i => i.CryptoCurrencyAbbreviation == selectedValueTo).Select(i => i.CryptoCurrencyName).FirstOrDefault();
            GetConversionRateAsync getConversionRateAsync = new GetConversionRateAsync();
            var conversionRate = getConversionRateAsync.GetConversionRate(selectedValueFrom, selectedValueTo);
            FeesManager fee = new FeesManager(_context);
            var currentFee = Convert.ToDouble((int)Math.Round((double)(Convert.ToDouble(fee.GetAllFees())/100) * amount));
            var bankAccount = _context.BankAccount.Where(i => i.IdUser == id && i.CurrencyName == fiatCurrencyName).FirstOrDefault();
            var cryptoAccount = _context.CryptoAccount.Where(i => i.CryptoCurrencyName == cryptoCurrencyName).FirstOrDefault();
            bankAccount.Sold -= (amount + currentFee);
            if (bankAccount.Sold < 0)
            {
                return null;
            }
            if (conversionRate == 0)
            {
                cryptoAccount.Sold = amount;
                _context.SaveChanges();
                return "ok";
            }
            var test = Convert.ToDouble(conversionRate);
            var test1 = test * amount;
            cryptoAccount.Sold += amount * Convert.ToDouble(conversionRate); 
            _context.SaveChanges();

            string type = "Fiat Transaction";
            CryptoManager transaction = new CryptoManager(_context, _mapper);
            transaction.AddCryptoTransaction(selectedValueFrom,selectedValueTo, amount, type);
            return "ok";
        }
        public string CryptoExchange(string body)
        {
            var cryptoCurrencyName = "";
            var fiatCurrencyName = "";

            JObject fieldData = JsonConvert.DeserializeObject<JObject>(body);
            int id = Convert.ToInt32(fieldData["id"]);
            string selectedValueFrom = Convert.ToString(fieldData["selectedValueFrom"]);
            string selectedValueTo = Convert.ToString(fieldData["selectedValueTo"]);
            double amount = Convert.ToDouble(fieldData["amountFrom"]);
            cryptoCurrencyName = _context.CryptoCurrency.Where(i => i.CryptoCurrencyAbbreviation == selectedValueFrom).Select(i => i.CryptoCurrencyName).FirstOrDefault();
            fiatCurrencyName = _context.Currency.Where(i => i.CurrencyAbbreviation == selectedValueTo).Select(i => i.CurrencyName).FirstOrDefault();
            if(fiatCurrencyName == null)
            {
                fiatCurrencyName = _context.CryptoCurrency.Where(i => i.CryptoCurrencyAbbreviation == selectedValueTo).Select(i => i.CryptoCurrencyName).FirstOrDefault();
                GetConversionRateAsync getConversionRateAsyncCrypto = new GetConversionRateAsync();
                double conversionRateCrypto = getConversionRateAsyncCrypto.GetConversionRate(selectedValueFrom, selectedValueTo);

                FeesManager feeCrypto = new FeesManager(_context);
                var currentFeeCrypto = Convert.ToDouble((int)Math.Round((double)(Convert.ToDouble(feeCrypto.GetAllFees()) / 100) * amount));

                var cryptoAccountFrom = _context.CryptoAccount.Where(i => i.CryptoCurrencyName == cryptoCurrencyName).FirstOrDefault();
                var cryptoAccountTo = _context.CryptoAccount.Where(i => i.CryptoCurrencyName == fiatCurrencyName).FirstOrDefault();
                cryptoAccountFrom.Sold -= (amount + currentFeeCrypto);
                if (cryptoAccountFrom.Sold < 0)
                {
                    return null;
                }
                cryptoAccountTo.Sold = amount * Convert.ToDouble(conversionRateCrypto);
                _context.SaveChanges();
                string type = "Crypto Transaction";
                CryptoManager transaction = new CryptoManager(_context, _mapper);
                transaction.AddCryptoTransaction(selectedValueFrom, selectedValueTo, amount, type);
                return "ok";
            }
            GetConversionRateAsync getConversionRateAsync = new GetConversionRateAsync();
            var conversionRate = getConversionRateAsync.GetConversionRate(selectedValueFrom, selectedValueTo);
            FeesManager fee = new FeesManager(_context);
            var currentFee = Convert.ToDouble((int)Math.Round((double)(Convert.ToDouble(fee.GetAllFees()) / 100) * amount));
            var acountTo = _context.BankAccount.Where(i => i.CurrencyName == fiatCurrencyName).FirstOrDefault();
            var acountFrom = _context.CryptoAccount.Where(i => i.CryptoCurrencyName == cryptoCurrencyName).FirstOrDefault();
            acountFrom.Sold -= (amount + currentFee);
            if (acountFrom.Sold < 0)
            {
                return null;
            }

            acountTo.Sold = amount * Convert.ToDouble(conversionRate);
            _context.SaveChanges();
            return "ok";
        }
        #endregion
    
        public double GetConversionRate(string from, string to)
        {
            GetConversionRateAsync getConversionRateAsync = new GetConversionRateAsync();
            var conversionRate = getConversionRateAsync.GetConversionRate(from, to);
            return conversionRate;
        }
    }
}
