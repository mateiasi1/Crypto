using iRepository;
using System;
using System.Linq;
using WebApplication17;
using WebApplication17.Data;
using WebApplication17.Models;

namespace BusinessLayer
{
    public class LoginManager : ILogin
    {
        protected Contexts _context;
        public LoginManager(Contexts context)
        {
            _context = context;
        }

        public Login AddLogin(Login login)
        {
            int userID = _context.User.Where(item => item.Username == login.Username).Select(item => item.Id).FirstOrDefault();

            var user = _context.User.Find(userID);

            var passwordSalt = user.PasswordSalt;
            string passwordHash = Hash.Create(login.Password, passwordSalt.ToString());

            if (user.Password == passwordHash)
            {
                var thisToken = new Token();

                thisToken.UserId = user.Id;
                thisToken.StartDate = DateTime.Now;
                thisToken.EndDate = DateTime.Now.AddMinutes(60);
                thisToken.TokenGuid = " ";

                login.Token = thisToken;
                _context.Token.Add(thisToken);
                _context.Login.Add(login);
                _context.SaveChanges();
               
            }
            return login;
        }

        public string DeleteLogin(string token)
        {
            Token deleteToken = _context.Token.FirstOrDefault(item => item.TokenGuid == token);
            _context.Remove(deleteToken);
            _context.SaveChanges();
            return token;
        }

        public Login GetLoginById(string username, string password)
        {
            var login = _context.Login.Where(u => u.Username == username && u.Password == password).FirstOrDefault();
            return login;
        }

        public Token RefreshToken(Token token)
        {
            Token updateToken = _context.Token.Find(token);
            updateToken.EndDate = DateTime.Now;
            _context.SaveChanges();
            return token;
        }
    }
}
