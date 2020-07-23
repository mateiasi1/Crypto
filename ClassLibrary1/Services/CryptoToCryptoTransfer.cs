using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebApplication17.Data;

namespace BusinessLayer.Services
{
    public class CryptoToCryptoTransfer : TransferBase
    {
        protected Contexts _context;
        protected IMapper _mapper;
        public CryptoToCryptoTransfer(Contexts context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;

        }
        public bool CryptoToCrypto { get; set; }
        public override bool DoTransfer(Transfer t)
        {
            var cryptoCurrencyName = _context.CryptoCurrency.Where(i => i.CryptoCurrencyAbbreviation == t.From).Select(i => i.CryptoCurrencyName).FirstOrDefault();
            var fiatCurrencyName = _context.Currency.Where(i => i.CurrencyAbbreviation == t.To).Select(i => i.CurrencyName).FirstOrDefault();
            if (fiatCurrencyName == null)
            {
                fiatCurrencyName = _context.CryptoCurrency.Where(i => i.CryptoCurrencyAbbreviation == t.To).Select(i => i.CryptoCurrencyName).FirstOrDefault();
                GetConversionRateAsync getConversionRateAsyncCrypto = new GetConversionRateAsync();
                double conversionRateCrypto = getConversionRateAsyncCrypto.GetConversionRate(t.To, t.From);

                FeesManager feeCrypto = new FeesManager(_context);
                var currentFeeCrypto = Convert.ToDouble((int)Math.Round((double)(Convert.ToDouble(feeCrypto.GetAllFees()) / 100) * t.Amount));

                var cryptoAccountFrom = _context.CryptoAccount.Where(i => i.CryptoCurrencyName == cryptoCurrencyName).FirstOrDefault();
                var cryptoAccountTo = _context.CryptoAccount.Where(i => i.CryptoCurrencyName == fiatCurrencyName).FirstOrDefault();
                cryptoAccountFrom.Sold -= (t.Amount + currentFeeCrypto);
                if (cryptoAccountFrom.Sold < 0)
                {
                    return false;
                }
                cryptoAccountTo.Sold = t.Amount * Convert.ToDouble(conversionRateCrypto);
                _context.SaveChanges();
                string type = "Crypto Transaction";
                CryptoManager transaction = new CryptoManager(_context, _mapper);
                transaction.AddCryptoTransaction(t.From, t.To, t.Amount, type);
                return true;
            }
            GetConversionRateAsync getConversionRateAsync = new GetConversionRateAsync();
            var conversionRate = getConversionRateAsync.GetConversionRate(t.From, t.To);
            FeesManager fee = new FeesManager(_context);
            var currentFee = Convert.ToDouble((int)Math.Round((double)(Convert.ToDouble(fee.GetAllFees()) / 100) * t.Amount));
            var acountTo = _context.BankAccount.Where(i => i.CurrencyName == fiatCurrencyName).FirstOrDefault();
            var acountFrom = _context.CryptoAccount.Where(i => i.CryptoCurrencyName == cryptoCurrencyName).FirstOrDefault();
            acountFrom.Sold -= (t.Amount + currentFee);
            if (acountFrom.Sold < 0)
            {
                return false;
            }

            acountTo.Sold = t.Amount * Convert.ToDouble(conversionRate);
            _context.SaveChanges();
            return true;
        }
    }
}
