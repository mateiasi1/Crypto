using iRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using WebApplication17;
using WebApplication17.Data;
using WebApplication17.Email;
using WebApplication17.Models;

namespace BusinessLayer
{
   public class RegisterUserManager : IRegisterUser
    {
        protected Contexts _context;
        EmailService emailService = new EmailService();
        string Body = System.IO.File.ReadAllText(("D:/DidacticalProjects/Crypto/backend/ClassLibrary1/Email/EmailTemplate.html"));

        public RegisterUserManager(Contexts context)
        {
            _context = context;
        }

        public RegisterUser AddUser(RegisterUser registerUser)
        {
            Random rnd = new Random();
            int referralRandom = rnd.Next(1000000, 9999999);
            registerUser.ReferralId = "CRYPTOAPP" + registerUser.Id.ToString() + registerUser.Username + referralRandom.ToString();
            Salt salt = new Salt();
            var passwordSalt = salt.ReturnSalt();
            string passwordHash = Hash.Create(registerUser.Password, passwordSalt.ToString());
            registerUser.PasswordSalt = passwordSalt.ToString();
            registerUser.PasswordHash = passwordHash;
            registerUser.Password = "admin";
            registerUser.PhoneNumber = " ";
            User user = new User();
            user.Password = registerUser.PasswordHash;
            user.Username = registerUser.Username;
            user.ReferralId = registerUser.ReferralId;
            user.PasswordSalt = passwordSalt.ToString();
            user.Confirmed = false;
            user.Role = registerUser.Role;
            //_context.RegisterUser.Add(registerUser);
            _context.User.Add(user);
             _context.SaveChanges();

            EmailModel model = new EmailModel();
            model.EmailTo = "dragosdm22@gmail.com";
            model.Subject = "test subject";
            model.Message = Body + "http://localhost:4200/validateAccount/" + user.Id + " " + user.Username + user.Token;
            model.UserId = user.Id;
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
