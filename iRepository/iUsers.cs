using System;
using System.Collections.Generic;
using System.Text;
using WebApplication17.Models;

namespace iRepository
{
    public interface iUsers
    {

        List<User> GetAllRegisteredUsers();
        User GetRegisterUserById(int id);
        List<User> GetConfirmedUsers();
        List<User> GetUnConfirmedUsers();
        bool SuspendUser(int id);
        bool ConfirmUser(int id);
        bool ResetPassword(int id);
        bool SetPassword(int id, string password);
        bool DeleteUser(int id);
        string GetRole(int id);



    }
}
