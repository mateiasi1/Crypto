using System;
using System.Collections.Generic;
using System.Text;
using WebApplication17.Models;

namespace iRepository
{
    public interface IRegisterUser
    {
        RegisterUser GetAllUsers();
        RegisterUser GetRegisterUserById(int id);
        RegisterUser AddUser(RegisterUser registerUser);
        RegisterUser DeleteUser(int id);
    }
}
