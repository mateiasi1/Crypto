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
    public class ConversionsController : ControllerBase
    {
        private readonly ConversionsManager _conversionsManager;
        private readonly IMapper _mapper;

        public ConversionsController(ConversionsManager conversionsManager, IMapper mapper)
        {
            _conversionsManager = conversionsManager;
            _mapper = mapper;
        }

        // GET: api/Conversions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Conversion>>> GetConversion()
        {
            _conversionsManager.GetAllConversions();
            return Ok(_mapper.Map<IEnumerable<ConversionDTO>>(_conversionsManager));
        }

        // POST: api/Conversions
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<Conversion>> PostConversion(Conversion conversion, string percentage)
        {
            var conversionList = _conversionsManager.AddConversion(conversion, percentage);
            return Ok(_mapper.Map<IEnumerable<BankDTO>>(conversionList));
        }
    }
}
