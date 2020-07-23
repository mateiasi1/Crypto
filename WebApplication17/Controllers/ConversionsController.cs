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

namespace DataLayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ConversionsController : CustomBaseController
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
        [HttpPost]
        public async Task<ActionResult<Conversion>> PostConversion(Conversion conversion, string percentage)
        {
            var conversionList = _conversionsManager.AddConversion(conversion, percentage);
            return Ok(_mapper.Map<IEnumerable<BankDTO>>(conversionList));
        }

        [HttpPut("exchangeFiat")]
        public IActionResult Exchange()
        {
            string body = this.InputBodyData;
            var conversionList = _conversionsManager.FiatExchange(body);
            return Ok();
        }


        [HttpPut("exchangeCrypto")]
        public IActionResult ExchangeCrypto()
        {
            string body = this.InputBodyData;
            var conversionList = _conversionsManager.CryptoExchange(body);
            return Ok();
        }

        [HttpPut("exchangeCryptoToFiat")]
        public IActionResult ExchangeCryptoToFiat()
        {
            string body = this.InputBodyData;
            var conversionList = _conversionsManager.ExchangeCryptoToFiat(body);
            return Ok();
        }
        // GET: api/ConversionsRate
        [HttpGet("{from}/{to}")]
        public double GetConversionRate(string from, string to)
        {
            double conversionRate =_conversionsManager.GetConversionRate(from, to);
            return conversionRate;
        }
       
    }
}
