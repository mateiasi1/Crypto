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
   public class RegisterUserManager : IRegisterUser
    {
        protected Contexts _context;
        private readonly AppSettings _appSettings;
        EmailService emailService = new EmailService();
        string Body = System.IO.File.ReadAllText(("D:/Didactical/backend/ClassLibrary1/Email/EmailTemplate.html"));

        public RegisterUserManager(Contexts context, IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
            _context = context;
        }

        public RegisterUser AddUser(RegisterUser registerUser)
        {
            User user = new User();
            Random rnd = new Random();
            int referralRandom = rnd.Next(1000000, 9999999);

            user.ReferralId = "CRYPTOAPP" + registerUser.Id.ToString() + registerUser.Username + referralRandom.ToString();
            user.Username = registerUser.Username;
            user.Confirmed = false;
            user.Role = "user";
            user.Email = registerUser.Email;
            user.IsOver18 = registerUser.IsOver18;
            user.FirstName = registerUser.FirstName;
            user.LastName = registerUser.LastName;

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
            user.Token = tokenHandler.WriteToken(token);
            _context.User.Add(user);
            _context.SaveChanges();

            EmailModel model = new EmailModel();
            model.EmailTo = registerUser.Email;
            model.Subject = "Activate account";
            model.Message = Body;
            model.Token = tokenHandler.WriteToken(token);
            model.Username = user.Username;
            emailService.SendEmail(model);
            return registerUser;
        }
        
        public List<RegisterUser> GetAllRegisteredUsers()
        {
            var registeredUsers = _context.RegisterUser.ToList();
            return registeredUsers;
        }

        public RegisterUser GetRegisterUserById(int id)
        {
            var registerUser =  _context.RegisterUser.Find(id);
            return registerUser;
        }

        public RegisterUser UpdateUserStatus(int id, RegisterUser registerUser)
        {
            _context.Entry(registerUser).State = EntityState.Modified;
            _context.SaveChanges();

            return registerUser;
        }
    }
}
