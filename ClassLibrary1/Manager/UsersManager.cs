using Data_Layer.Models;
using iRepository;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using DataLayer;
using DataLayer.Email;
using WebApplication17.Data;
using WebApplication17.Models;
using BusinessLayer.DTO;
using DataLayer.DTO;
using AutoMapper;
using DataLayer.Models;

namespace BusinessLayer
{
    public class UsersManager : IUsers
    {
        protected Contexts _context;
        private readonly IMapper _mapper;

        private readonly AppSettings _appSettings;
        readonly EmailService emailService = new EmailService();
        string Body = System.IO.File.ReadAllText(("D:/DidacticalProjects/Crypto/backend/ClassLibrary1/Email/EmailTemplate.html"));
        public ListDTO<UserDTO> list = new ListDTO<UserDTO>();

        public UsersManager(Contexts context, IOptions<AppSettings> appSettings, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
            _appSettings = appSettings.Value;
        }

        public List<User> GetAllRegisteredUsers()
        {
            var users = _context.User.Where(u => u.Confirmed == true).ToList();
            return users;
        }

        public List<User> GetConfirmedUsers()
        {
            var users = (_context.User.Where(u => u.Confirmed == true)).ToList();
            return users;
        }

        public ListDTO<UserDTO> GetRegisterUserById(int id)
        {
            list.Items = new List<UserDTO>();
            var user = _context.User.Find(id);
            var items = _mapper.Map<UserDTO>(user);
            list.Items.Add(items);

            return list;
        }

        public List<User> GetUnConfirmedUsers()
        {
            var users = (_context.User.Where(u => u.Confirmed == false)).ToList();
            return users;
        }

        public bool SuspendUser(int id)
        {
            var user =  _context.User.Find(id);
            if(user == null)
            {
                return false;
            }
            user.Confirmed = false;
            _context.SaveChanges();
            return true;
        }

        public bool ConfirmUser(int id)
        {
            var user = _context.User.Find(id);
            if (user == null)
            {
                return false;
            }
            Random rnd = new Random();
            int referralRandom = rnd.Next(1000000, 9999999);

            user.ReferralId = "CRYPTOAPP" + user.Id.ToString() + user.Username + referralRandom.ToString();
            user.Confirmed = true;
            _context.SaveChanges();
            return true;
        }

        public bool ResetPassword(int id)
        {
            var user = _context.User.Find(id);
            if (user == null)
            {
                return false;
            }
            user.Password = null;

            _context.SaveChanges();

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            EmailModel model = new EmailModel();
            model.EmailTo = "dragosdm22@gmail.com";
            model.Subject = "Activate account";
            model.Token = tokenHandler.WriteToken(token);
            model.Username = user.Username;
            emailService.SendEmail(model);

            return true;
        }

        public bool SetPassword(PasswordToSet password)
        {
            var user = _context.User.Where(u=> u.Token == password.Token).FirstOrDefault();
            if (user != null)
            {
                Salt salt = new Salt();
                var passwordSalt = salt.ReturnSalt();
                string passwordHash = Hash.Create(password.Password, passwordSalt.ToString());
                user.PasswordSalt = passwordSalt.ToString();
                user.Password = passwordHash;
                _context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool DeleteUser(int id)
        {
            var user = _context.User.Find(id);
            if (user == null)
            {
                return false;
            }

            _context.User.Remove(user);
             _context.SaveChanges();
            return true;
        }

        public string GetRole(int id)
        {
            var user = _context.User.Find(id);
            if(user == null)
            {
                return null;
            }
            return user.Role;
        }

        public bool ForgotPassword(int id)
        {
            var user = _context.User.Find(id);
            if (user == null)
            {
                return false;
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            EmailModel model = new EmailModel();
            model.EmailTo = "dragosdm22@gmail.com";
            model.Subject = "Activate account";
            model.Token = tokenHandler.WriteToken(token);
            model.Username = user.Username;
            emailService.SendEmail(model);

            return true;
        }

        public ListDTO<UserDTO> ChangeUser(User user)
        {
            list.Items = new List<UserDTO>();
            User userFromDb = _context.User.Where(u => u.Username == user.Username).FirstOrDefault();
            userFromDb.Email = user.Email;
            userFromDb.FirstName = user.FirstName;
            userFromDb.LastName = user.LastName;
            _context.SaveChanges();
            var items = _mapper.Map<UserDTO>(user);
            list.Items.Add(items);
            return list;
        }

        public ListDTO<ChangePasswordDTO> ChangePassword(ChangePassword password)
        {
            ListDTO<ChangePasswordDTO> listPasswordChange = new ListDTO<ChangePasswordDTO>();

            listPasswordChange.Items = new List<ChangePasswordDTO>();

            var user = _context.User.Where(u => u.Id == password.UserId).FirstOrDefault();
            if (password.NewPassword != password.ConfirmPassword)
            {
                listPasswordChange.Items = null;
                return listPasswordChange;
            }
            if (user != null)
            {
                string passwordHash = Hash.Create(password.Password, user.PasswordSalt);
                password.Password = passwordHash;
            }
            if (user.Password == password.Password)
            {
                Salt salt = new Salt();
                var passwordSalt = salt.ReturnSalt();
                string passwordHash = Hash.Create(password.NewPassword, passwordSalt.ToString());
                user.PasswordSalt = passwordSalt.ToString();
                user.Password = passwordHash;
                _context.SaveChanges();
                password.Password = user.Password;
                var items = _mapper.Map<ChangePasswordDTO>(password);
                listPasswordChange.Items.Add(items);
            }
            
            return listPasswordChange;
        }

        public ListDTO<TransferDTO> Transfer(Transfer transfer)
        { 
            ListDTO<TransferDTO> transferList = new ListDTO<TransferDTO>();
            transferList.Items = new List<TransferDTO>();
            var userFrom = _context.User.Where(u => u.Id == transfer.IdUserFrom).FirstOrDefault();
            var userTo = _context.User.Where(u => u.ReferralId == transfer.RefferalUserTo).FirstOrDefault();
            var userAccountFrom = _context.CryptoAccount.Where(c => c.IdUser == userFrom.Id && c.CryptoCurrencyName == transfer.CoinName).FirstOrDefault();
            var userAccountTo = _context.CryptoAccount.Where(c => c.IdUser == userTo.Id && c.CryptoCurrencyName == transfer.CoinName).FirstOrDefault();
            if ((userAccountFrom  == null) || (userAccountTo == null))
            {
                transferList.Items = null;
                return transferList;
            }
            userAccountFrom.Sold -= transfer.Amount;
            userAccountTo.Sold += transfer.Amount;
            transfer.Date = DateTime.Now;

            string type = "Transfer to " + userTo.Username;
            CryptoManager transaction = new CryptoManager(_context, _mapper);
            transaction.AddCryptoTransaction(transfer.CoinName, transfer.CoinName, transfer.Amount, type);

            _context.Transfer.Add(transfer);
            _context.SaveChanges();
            var items = _mapper.Map<TransferDTO>(transfer);
            transferList.Items.Add(items);
            return transferList;
        }
    }
}
