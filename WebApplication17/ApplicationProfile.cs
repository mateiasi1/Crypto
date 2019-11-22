using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication17.DTO;
using WebApplication17.Models;

namespace WebApplication17
{
    public class ApplicationProfile : AutoMapper.Profile
    {
        public  ApplicationProfile()
            {
            CreateMap<Bank, BankDTO>().ReverseMap();
            CreateMap<BankAccount, BankAccountDTO>().ReverseMap();
            CreateMap<BankAccountTransaction, BankAccountTransactionDTO>().ReverseMap();
            CreateMap<Conversion, ConversionDTO>().ReverseMap();
            CreateMap<ConversionTransaction, ConversionTransactionDTO>().ReverseMap();
            CreateMap<Crypto, CryptoDTO>().ReverseMap();
            CreateMap<Currency, CurrencyDTO>().ReverseMap();
            CreateMap<Fee, FeeDTO>().ReverseMap();
            CreateMap<FlatRateFee, FlatRateFeeDTO>().ReverseMap();
            CreateMap<Login, LoginDTO>().ReverseMap();
            CreateMap<RegisterUser, RegisterUserDTO>().ReverseMap();
            //CreateMap<Token, TokenDTO>().ReverseMap();
            CreateMap<User, UserDTO>().ReverseMap();
            CreateMap<Wallet, WalletDTO>().ReverseMap();
            CreateMap<User, UnconfirmedUsersDTO>().ReverseMap();
            CreateMap<Crypto, CryptoDTO>().ReverseMap();
            CreateMap<CryptoAccount, CryptoAccountDTO>().ReverseMap();
        }
    }
}
