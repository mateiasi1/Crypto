using System;
using System.Collections.Generic;
using System.Text;
using WebApplication17.Models;

namespace iRepository
{
    public interface ILogin
    {
        Login GetLoginById(string username, string password);
        int DeleteLogin(int id);
        Token RefreshToken(Token token);
        User Authenticate(string username, string password);
        IEnumerable<User> GetAll();
    }
}
