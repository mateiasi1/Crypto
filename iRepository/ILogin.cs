using System;
using System.Collections.Generic;
using System.Text;
using WebApplication17.Models;

namespace iRepository
{
    public interface ILogin
    {
        Login GetLoginById(string username, string password);
        Login AddLogin(Login login);
        string DeleteLogin(string token);
        Token RefreshToken(Token token);
    }
}
