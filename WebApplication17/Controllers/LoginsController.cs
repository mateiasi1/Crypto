using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BusinessLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication17.Data;
using WebApplication17.DTO;
using WebApplication17.Models;

namespace WebApplication17.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginsController : ControllerBase
    {
        private readonly Contexts _context;
        private readonly IMapper _mapper;
        private readonly LoginManager _loginManager;

        public LoginsController(Contexts context, IMapper mapper, LoginManager loginManager)
        {
            _mapper = mapper;
            _context = context;
            _loginManager = loginManager;
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

        // POST: api/Logins
        [HttpPost]
        public async Task<ActionResult<Login>> PostLogin([FromBody]Login login)
        {
            var loginList = _loginManager.AddLogin(login);
            return Ok(_mapper.Map<IEnumerable<LoginDTO>>(loginList));
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
