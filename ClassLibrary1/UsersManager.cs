using Data_Layer.Models;
using iRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using WebApplication17;
using WebApplication17.Data;
using WebApplication17.Email;
using WebApplication17.Models;

namespace BusinessLayer
{
    public class UsersManager : IUsers
    {
        protected Contexts _context;

        private readonly AppSettings _appSettings;
        readonly EmailService emailService = new EmailService();
        string Body = System.IO.File.ReadAllText(("D:/DidacticalProjects/Crypto/backend/ClassLibrary1/Email/EmailTemplate.html"));
        

        public UsersManager(Contexts context, IOptions<AppSettings> appSettings)
        {
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

        public User GetRegisterUserById(int id)
        {
            var users = _context.User.Find(id);
            return users;
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

        public bool SetPassword(int id, string password)
        {
            var user = _context.User.Find(id);
            if (user != null)
            {
                Salt salt = new Salt();
                var passwordSalt = salt.ReturnSalt();
                string passwordHash = Hash.Create(user.Password, passwordSalt.ToString());
                user.PasswordSalt = passwordSalt.ToString();
                user.Password = passwordHash;
                _context.SaveChangesAsync();

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
    }
}
