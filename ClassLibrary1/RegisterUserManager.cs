using iRepository;
using System;
using System.Collections.Generic;
using System.Text;
using WebApplication17.Data;

namespace BusinessLayer
{
    class RegisterUserManager : IRegisterUser
    {
        protected Contexts _context;
        public RegisterUserManager(Contexts context)
        {
            _context = context;
        }

    }
}
