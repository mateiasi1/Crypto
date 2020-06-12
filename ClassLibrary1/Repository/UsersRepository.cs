using BusinessLayer.DTO;
using Data_Layer.Models;
using DataLayer.DTO;
using System.Collections.Generic;
using WebApplication17.Models;

namespace iRepository
{
    public interface IUsers
    {
        List<User> GetAllRegisteredUsers();
        ListDTO<UserDTO> GetRegisterUserById(int id);
        List<User> GetConfirmedUsers();
        List<User> GetUnConfirmedUsers();
        bool SuspendUser(int id);
        bool ConfirmUser(int id);
        bool ResetPassword(int id);
        bool SetPassword(PasswordToSet password);
        bool DeleteUser(int id);
        string GetRole(int id);
        bool ForgotPassword(int id);
        ListDTO<UserDTO> ChangeUser(User user);
    }
}
