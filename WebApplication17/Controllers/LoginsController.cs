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

        public LoginsController(Contexts context, IMapper mapper, LoginManager loginManager)
        {
            _mapper = mapper;
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
        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult<Login>> PostLogin([FromBody]Login login)
        {
            var loginList = _loginManager.AddLogin(login);
            if(loginList == null)
            {
                return BadRequest(new { message ="Username or password is incorrect"});
            }
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
