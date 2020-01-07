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
using WebApplication17.Data;
using WebApplication17.DTO;
using WebApplication17.Models;

namespace WebApplication17.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class LoginsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly LoginManager _loginManager;
        private ILogin _userService;

        public LoginsController(Contexts context, IMapper mapper, LoginManager loginManager, ILogin userService)
        {
            _mapper = mapper;
            _loginManager = loginManager;
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody]User userParam)
        {
            var user = _userService.Authenticate(userParam.Username, userParam.Password);

            if (user == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(user);
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var users = _userService.GetAll();
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
        [HttpDelete("{token}")]
        public async Task<ActionResult<Login>> DeleteLogin(string token)
        {
            _loginManager.DeleteLogin(token);
            return Ok();
        }
    }
}
