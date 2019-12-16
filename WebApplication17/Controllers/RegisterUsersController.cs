using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BusinessLayer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication17.Data;
using WebApplication17.DTO;
using WebApplication17.Email;
using WebApplication17.Models;

namespace WebApplication17.Controllers
{
   
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterUsersController : ControllerBase
    {
        private readonly Contexts _context;
        private readonly IMapper _mapper;
        private readonly RegisterUserManager _registerUserManager;

        EmailService emailService = new EmailService();
        string Body = System.IO.File.ReadAllText(("Email/EmailTemplate.html"));
        

        public RegisterUsersController(Contexts context, IMapper mapper, RegisterUserManager registerUserManager)
        {
            _mapper = mapper;
            _context = context;
            _registerUserManager = registerUserManager;
        }

        // GET: api/RegisterUsers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RegisterUser>>> GetRegisterUser()
        {
            var registerUsers = _registerUserManager.GetAllRegisteredUsers();
            return Ok(_mapper.Map<IEnumerable<RegisterUser>>(registerUsers));
        }

        // GET: api/RegisterUsers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RegisterUser>> GetRegisterUser(int id)
        {
            var registerUsers = _registerUserManager.GetRegisterUserById(id);
            return Ok(registerUsers);
        }

        // PUT: api/RegisterUsers/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRegisterUser(int id, RegisterUser registerUser)
        {
            var modifiedUser = _registerUserManager.UpdateUserStatus(id, registerUser);
            return Ok(modifiedUser);
        }

        // POST: api/RegisterUsers
        [HttpPost]
        public async Task<ActionResult<RegisterUser>> PostRegisterUser([FromBody]RegisterUser registerUser)
        {
            var registeredUser = _registerUserManager.AddUser(registerUser);
            return Ok(_mapper.Map<IEnumerable<RegisterUserDTO>>(registeredUser));
        }
        
    }
}
