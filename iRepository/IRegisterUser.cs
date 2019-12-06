using System;
using System.Collections.Generic;
using System.Text;
using WebApplication17.Models;

namespace iRepository
{
    public interface IRegisterUser
    {
        List<RegisterUser> GetAllRegisteredUsers();
        RegisterUser GetRegisterUserById(int id);
        RegisterUser AddUser(RegisterUser registerUser);
        RegisterUser UpdateUserStatus(int id, RegisterUser registerUser);
    }
}
