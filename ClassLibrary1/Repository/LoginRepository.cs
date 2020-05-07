using System.Collections.Generic;
using WebApplication17.Models;

namespace iRepository
{
    public interface LoginRepository
    {
        Login GetLoginById(string username, string password);
        int DeleteLogin(int id);
        Token RefreshToken(Token token);
        User Authenticate(string username, string password);
        IEnumerable<User> GetAll();
    }
}
