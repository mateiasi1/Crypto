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
using BusinessLayer.DTO;
using DataLayer.Models;

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

        [HttpGet("{id}")]
        public IActionResult GetUserProfile(int id)
        {
            ResponseDTO<UserDTO> response = new ResponseDTO<UserDTO>();
            ListDTO<UserDTO> list = new ListDTO<UserDTO>();
            list = _userManager.GetRegisterUserById(id);
            response.Data = new ListDTO<UserDTO>();
            if (list != null)
            {
                response.Data = list;
                response.Message = "List is retrieved successfully";
                response.Success = true;
                return Ok(response);
            }
            response.Data = null;
            response.Message = "List is not retrieved successfully";
            response.Success = false;

            return NotFound(response);
        }

        [HttpPost("change")]
        public IActionResult ChangeUser(User user)
        {
            ResponseDTO<UserDTO> response = new ResponseDTO<UserDTO>();
            ListDTO<UserDTO> list = new ListDTO<UserDTO>();
            list = _userManager.ChangeUser(user);
            response.Data = new ListDTO<UserDTO>();
            if (list != null)
            {
                response.Data = list;
                response.Message = "Your user is updated!";
                response.Success = true;
                return Ok(response);
            }
            response.Data = null;
            response.Message = "An error occured, please try again!";
            response.Success = false;

            return NotFound(response);
        }

        [HttpPost("changePassword")]
        public IActionResult ChangePassword(ChangePassword password)
        {
            ResponseDTO<ChangePasswordDTO> response = new ResponseDTO<ChangePasswordDTO>();
            ListDTO<ChangePasswordDTO> list = new ListDTO<ChangePasswordDTO>();
            list = _userManager.ChangePassword(password);
            response.Data = new ListDTO<ChangePasswordDTO>();
            if (list != null)
            {
                response.Data = list;
                response.Message = "Your user is updated!";
                response.Success = true;
                return Ok(response);
            }
            response.Data = null;
            response.Message = "An error occured, please try again!";
            response.Success = false;

            return NotFound(response);
        }

        [HttpPost("transfer")]
        public IActionResult Transfer(Transfer transfer)
        {
            ResponseDTO<TransferDTO> response = new ResponseDTO<TransferDTO>();
            ListDTO<TransferDTO> list = new ListDTO<TransferDTO>();
            list = _userManager.Transfer(transfer);
            response.Data = new ListDTO<TransferDTO>();
            if (list != null)
            {
                response.Data = list;
                response.Message = "Transfer was made!";
                response.Success = true;
                return Ok(response);
            }
            response.Data = null;
            response.Message = "An error occured, please try again!";
            response.Success = false;

            return NotFound(response);
        }
    }
}
