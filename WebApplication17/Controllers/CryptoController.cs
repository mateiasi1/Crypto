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

using DataLayer.DTO;
using WebApplication17.Models;
using BusinessLayer.DTO;

namespace DataLayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CryptoController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly CryptoManager _cryptoManager;

        public CryptoController(IMapper mapper, CryptoManager cryptoManager)
        {
            _mapper = mapper;
            _cryptoManager = cryptoManager;

        }

        // GET: api/Banks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Crypto>>> GetCrypto()
        {
            ResponseDTO<CryptoDTO> response = new ResponseDTO<CryptoDTO>();
            ListDTO<CryptoDTO> list = new ListDTO<CryptoDTO>();

            list = _cryptoManager.GetAllCrypto();
            response.Data = new ListDTO<CryptoDTO>();
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

        // GET: api/Banks/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Crypto>> GetCrypto(int id)
        {
            ResponseDTO<CryptoDTO> response = new ResponseDTO<CryptoDTO>();
            ListDTO<CryptoDTO> list = new ListDTO<CryptoDTO>();

            var crypto = _cryptoManager.GetCryptoById(id);

            if (crypto == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<IEnumerable<CryptoDTO>>(_cryptoManager));
        }


        // POST: api/Banks
        [HttpPost]
        public async Task<ActionResult<Crypto>> PostCrypto(Crypto crypto)
        {
            ResponseDTO<CryptoDTO> response = new ResponseDTO<CryptoDTO>();
            ListDTO<CryptoDTO> list = new ListDTO<CryptoDTO>();
            try
            {
                var cryptoList = _cryptoManager.AddCrypto(crypto);
                return Ok(_mapper.Map<IEnumerable<CryptoDTO>>(cryptoList));
            }
            catch (Exception ex)
            {
                throw new System.Exception(ex.Message);
            }

        }

        // DELETE: api/Banks/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Crypto>> DeleteCrypto(int id)
        {
            ResponseDTO<CryptoDTO> response = new ResponseDTO<CryptoDTO>();
            ListDTO<CryptoDTO> list = new ListDTO<CryptoDTO>();

            var cryptoList = _cryptoManager.DeleteCrypto(id);
            return Ok(_mapper.Map<IEnumerable<CryptoDTO>>(cryptoList));
        }
    }
}
