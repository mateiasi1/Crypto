using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using WebApplication17.Data;
using WebApplication17.DTO;
using WebApplication17.Models;

namespace WebApplication17.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GetCryptoCurrencyAPI : CustomBaseController
    {
        private readonly Contexts _context;
        private readonly IMapper _mapper;

        public GetCryptoCurrencyAPI(Contexts context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }
        // GET: /<controller>/
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CryptoCurrency>>> GetCryptoCurrencyFromAPI()
        {
            List<CryptoCurrency> cryptoCurrencyList = new List<CryptoCurrency>();
            using (var client = new HttpClient())
            {
                try
                {
                    client.BaseAddress = new Uri("https://min-api.cryptocompare.com/data/all/coinlist");
                    var response = await client.GetAsync($"?access_key=6a68fbb7153ff890b106018dd642c8bb");
                    response.EnsureSuccessStatusCode();
                    var stringResult = await response.Content.ReadAsStringAsync();
                    CryptoCurrency cryptoCurrency = new CryptoCurrency();

                    JObject field = JsonConvert.DeserializeObject<JObject>(stringResult);
                    JObject fieldData = JsonConvert.DeserializeObject<JObject>(field["Data"].ToString());
                    foreach ( var item in fieldData)
                    {
                        cryptoCurrency.Id = 0;
                        cryptoCurrency.CryptoCurrencyName = (item["FullName"]).ToString();
                        cryptoCurrency.CryptoCurrencyAbbreviation = (item["Symbol"]).ToString();
                        cryptoCurrencyList.Add(cryptoCurrency);
                    }

                    return Ok(_mapper.Map<IEnumerable<CryptoCurrencyDTO>>(cryptoCurrencyList));

                }
                catch (HttpRequestException httpRequestException)
                {
                    return BadRequest($"Error getting crypto: {httpRequestException.Message}");
                }
            }
        }
    }
}
