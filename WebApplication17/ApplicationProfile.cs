using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLayer.DTO;
using WebApplication17.Models;

namespace DataLayer
{
    public class ApplicationProfile : AutoMapper.Profile
    {
        public  ApplicationProfile()
            {
            CreateMap<Bank, BankDTO>().ReverseMap();
            CreateMap<BankDTO, Bank>().ReverseMap();
            CreateMap<BankAccount, BankAccountDTO>().ReverseMap();
            CreateMap<BankAccountDTO, BankAccount>().ReverseMap();
            CreateMap<BankAccountTransaction, BankAccountTransactionDTO>().ReverseMap();
            CreateMap<BankAccountTransactionDTO, BankAccountTransaction>().ReverseMap();
            CreateMap<Conversion, ConversionDTO>().ReverseMap();
            CreateMap<ConversionDTO, Conversion>().ReverseMap();
            CreateMap<ConversionTransaction, ConversionTransactionDTO>().ReverseMap();
            CreateMap<ConversionTransactionDTO, ConversionTransaction>().ReverseMap();
            CreateMap<Crypto, CryptoDTO>().ReverseMap();
            CreateMap<CryptoDTO, Crypto>().ReverseMap();
            CreateMap<Currency, CurrencyDTO>().ReverseMap();
            CreateMap<CurrencyDTO, Currency>().ReverseMap();
            CreateMap<Fee, FeeDTO>().ReverseMap();
            CreateMap<FeeDTO, Fee>().ReverseMap();
            CreateMap<FlatRateFee, FlatRateFeeDTO>().ReverseMap();
            CreateMap<FlatRateFeeDTO, FlatRateFee>().ReverseMap();
            CreateMap<Login, LoginDTO>().ReverseMap();
            CreateMap<LoginDTO, Login>().ReverseMap();
            CreateMap<RegisterUser, RegisterUserDTO>().ReverseMap();
            CreateMap<RegisterUserDTO, RegisterUser>().ReverseMap();
            //CreateMap<Token, TokenDTO>().ReverseMap();
            CreateMap<User, UserDTO>().ReverseMap();
            CreateMap<UserDTO, User>().ReverseMap();
            CreateMap<Wallet, WalletDTO>().ReverseMap();
            CreateMap<WalletDTO, Wallet>().ReverseMap();
            CreateMap<User, UnconfirmedUsersDTO>().ReverseMap();
            CreateMap<Crypto, CryptoDTO>().ReverseMap();
            CreateMap<CryptoDTO, Crypto>().ReverseMap();
            CreateMap<CryptoAccount, CryptoAccountDTO>().ReverseMap();
            CreateMap<CryptoAccountDTO, CryptoAccount>().ReverseMap();
            CreateMap<CryptoCurrency, CryptoCurrencyDTO>().ReverseMap();
            CreateMap<CryptoCurrencyDTO, CryptoCurrency>().ReverseMap();
            CreateMap<User, LoginDTO>().ReverseMap();
            CreateMap<UserDTO, Login>().ReverseMap();
        }
    }
}
