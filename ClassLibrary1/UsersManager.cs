using iRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebApplication17;
using WebApplication17.Data;
using WebApplication17.Email;
using WebApplication17.Models;

namespace BusinessLayer
{
    public class UsersManager : iUsers
    {
        readonly EmailService emailService = new EmailService();
        string Body = System.IO.File.ReadAllText(("D:/DidacticalProjects/Crypto/backend/ClassLibrary1/Email/EmailTemplate.html"));
        protected Contexts _context;

        public UsersManager(Contexts context)
        {
            _context = context;
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

            EmailModel model = new EmailModel();
            model.EmailTo = "dragosdm22@gmail.com";
            model.Subject = "test subject";
            model.Message = Body + "http://localhost:4200/validateAccount/" + user.Id + " " + user.Username + user.Token;
            model.UserId = user.Id;
            model.Username = user.Username;
            emailService.SendEmail(model);

            return true;
        }

        public bool SetPassword(int id, string password)
        {
            var user = _context.User.Find(id);
            if (user != null)
            {
                var passwordSalt = new Salt();
                string passwordHash = Hash.Create(user.Password, passwordSalt.ToString());
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
    }
}
