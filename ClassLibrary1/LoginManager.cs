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
using WebApplication17;
using WebApplication17.Data;
using WebApplication17.Models;

namespace BusinessLayer
{
    public class LoginManager : ILogin
    {
        protected Contexts _context;
        private readonly AppSettings _appSettings;
        private List<User> _users = new List<User>
        {
            new User { Id = 1, Username = "test", Password = "test" }
        };
        public LoginManager(IOptions<AppSettings> appSettings, Contexts context)
        {
            _context = context;
            _appSettings = appSettings.Value;
        }

        public User Authenticate(string username, string password)
        {
            int userID = _context.User.Where(item => item.Username == username).Select(item => item.Id).FirstOrDefault();
            var user = _context.User.Find(userID);

            

            // return null if user not found
            if (user == null)
                return null;
            //return null if the user is not confirmed by admin
            if (user.Confirmed == false)
            {
                return null;
            }

            // authentication successful so generate jwt token
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
           
            var passwordSalt = user.PasswordSalt;
            string passwordHash = Hash.Create(password, passwordSalt.ToString());
            if (user.Password == passwordHash)
            {
                _context.SaveChanges();
                // remove password before returning
                user.Password = null;
                return user;
            }

            return null;
            
        }

        public IEnumerable<User> GetAll()
        {
            // return users without passwords
            return _users.Select(x => {
                x.Password = null;
                return x;
            });
        }

        public int DeleteLogin(int id)
        {
            Token deleteToken = _context.Token.FirstOrDefault(item => item.Id == id);
            User user = _context.User.FirstOrDefault(i => i.Id == id);
            user.Token = null;
            _context.SaveChanges();
            return id;
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
