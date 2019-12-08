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
    [Route("api/[controller]")]
    [ApiController]
    public class CryptoController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly Contexts _context;
        private readonly CryptoManager _cryptoManager;

        public CryptoController(Contexts context, IMapper mapper, CryptoManager cryptoManager)
        {
            _context = context;
            _mapper = mapper;
            _cryptoManager = cryptoManager;
        }

        // GET: api/Banks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Crypto>>> GetCrypto()
        {
            return _cryptoManager.GetAllCrypto();
        }

        // GET: api/Banks/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Crypto>> GetCrypto(int id)
        {
            var crypto = _cryptoManager.GetCryptoById(id);

            if (crypto == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<IEnumerable<CryptoDTO>>(_cryptoManager));
        }


        // POST: api/Banks
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<Crypto>> PostCrypto(Crypto crypto)
        {

            var cryptoList = _cryptoManager.AddCrypto(crypto);
            return Ok(_mapper.Map<IEnumerable<CryptoDTO>>(cryptoList));
        }

        // DELETE: api/Banks/5
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<ActionResult<Crypto>> DeleteCrypto(int id)
        {
            var cryptoList = _cryptoManager.DeleteCrypto(id);
            return Ok(_mapper.Map<IEnumerable<CryptoDTO>>(cryptoList));
        }

        private bool CryptoExists(int id)
        {
            return _context.Crypto.Any(e => e.Id == id);
        }
    }
}
