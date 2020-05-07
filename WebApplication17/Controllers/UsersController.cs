using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BusinessLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using DataLayer.DTO;
using iRepository;
using WebApplication17.Models;
using Data_Layer.Models;

namespace DataLayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly UsersManager _userManager;

        public UsersController(IMapper mapper, UsersManager userManager)
        {
            _mapper = mapper;
            _userManager = userManager;
        }
        [HttpGet]
        public string GetUserRole(int id)
        {
            string role = _userManager.GetRole(id);
            return role;
        }
        // GET: api/Users/confirmed
        [HttpGet("confirmed")]
        public IActionResult GetConfirmedUsers()
        {
            var users = _userManager.GetConfirmedUsers();
            return Ok(_mapper.Map<IEnumerable<UnconfirmedUsersDTO>>(users));
        }

        [HttpGet("unconfirmed")]
        public IActionResult GetUnconfirmedUsers()
        {
            var users = _userManager.GetUnConfirmedUsers();
            return Ok(_mapper.Map<IEnumerable<UnconfirmedUsersDTO>>(users));
        }

        // PUT: api/Users
        [HttpPut]
        public async Task<IActionResult> ResetPassword([FromBody]int id)
        {
            if (_userManager.ResetPassword(id))
            {
                return Ok();
            }
            // de legat de serviciul de email si de trimis GUID + Id in link ul de email pentru resetare
            return BadRequest();
        }

        // PUT: api/Users/confirmed
        [HttpPut("{Confirmed}")]
        public async Task<IActionResult> ConfirmUser([FromBody]int id)
        {
            if (_userManager.ConfirmUser(id))
            {
                return Ok();
            }
            return BadRequest();
        }

        [HttpPut("suspend")]
        public async Task<ActionResult> SuspendUser([FromBody]int id)
        {
            if (_userManager.SuspendUser(id))
            {
                return Ok();
            }
            return BadRequest();
        }

        [HttpPut("setPassword")]
        public async Task<ActionResult> SetPassword([FromBody]PasswordToSet password)
        {
            if (_userManager.SetPassword(password))
            {
                return Ok();
            }
            return BadRequest();
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUser(int id)
        {
            if (_userManager.DeleteUser(id))
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost("forgot")]
        public async Task<ActionResult<User>> ForgotPassword([FromBody] int id)
        {
            _userManager.ForgotPassword(id);
            return Ok();
        }

    }
}
