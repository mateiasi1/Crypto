using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BusinessLayer;
using iRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using DataLayer.DTO;
using WebApplication17.Data;
using Microsoft.AspNetCore.Cors;
using WebApplication17.Models;

namespace DataLayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly LoginManager _loginManager;
        private ILogin _userService;

        public LoginsController(Contexts context, IMapper mapper, LoginManager loginManager)
        {
            _mapper = mapper;
            _loginManager = loginManager;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        //[EnableCors("AllowOrigin")]
        public IActionResult Authenticate([FromBody]User userParam)
        {
            if (userParam.Username == null || userParam.Password == null)
            {
                return BadRequest(new { message = "Username or password is incorrect" });
            }
               
            var user = _loginManager.Authenticate(userParam.Username, userParam.Password);
            if(user != null)
            {

                return Ok(_mapper.Map<LoginDTO>(user));
            }
            return Unauthorized(user);
        }

        [HttpGet]
        public IActionResult GetAll([FromBody] User userParam)
        {
            var users = _loginManager.GetAll();
            return Ok(users);
        }

        // GET: api/Banks/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Login>> GetLogin(string username, string password)
        {
            var login = _loginManager.GetLoginById(username, password);
            return login;
        }

        // PUT: api/Banks/token
        [HttpPut("{token}")]
        public async Task<IActionResult> PutLogin(Token token)
        {
            var tokens = _loginManager.RefreshToken(token);
            return Ok(_mapper.Map<IEnumerable<Token>>(tokens));
        }

        // DELETE: api/Login/5
        [AllowAnonymous]
        [HttpDelete("{id}")]
        public async Task<ActionResult<Login>> DeleteLogin(int id)
        {
            _loginManager.DeleteLogin(id);
            return Ok();
        }
    }
}
