using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using WebApplication17.Data;
using WebApplication17.DTO;
using WebApplication17.Models;

namespace WebApplication17.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class GetFiatCurrencyAPI : Controller
    {

        private readonly Contexts _context;
        private readonly IMapper _mapper;
        public GetFiatCurrencyAPI(Contexts context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }
        // GET: /<controller>/
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Currency>>> GetCurrencyFromAPI()
        {
            List<Currency> currency = new List<Currency>();
            using (var client = new HttpClient())
            {
                try
                {
                    client.BaseAddress = new Uri("http://apilayer.net/api/list");
                    var response = await client.GetAsync($"?access_key=6a68fbb7153ff890b106018dd642c8bb");
                    response.EnsureSuccessStatusCode();

                    var stringResult = await response.Content.ReadAsStringAsync();
                    var jsonData = JObject.Parse(stringResult);
                    foreach (var item in jsonData)
                    {
                        if (item.Key != "currencies")
                        {
                            continue;
                        }

                        foreach (var items in item.Value)
                        {
                            var itemValueResult = item.Value.ToString();
                            var jsonItemValue = JObject.Parse(itemValueResult);
                            foreach (var valueItems in jsonItemValue)
                            {
                                Currency currencyObject = new Currency();
                                currencyObject.CurrencyAbbreviation = valueItems.Key;
                                currencyObject.CurrencyName = valueItems.Value.ToString();
                                currency.Add(currencyObject);
                            }
                            break;
                        }
                    }
                    return Ok(_mapper.Map<IEnumerable<CurrencyDTO>>(currency));
                }
                catch (HttpRequestException httpRequestException)
                {
                    return BadRequest($"Error getting crypto: {httpRequestException.Message}");
                }
            }
        }

    }
}
