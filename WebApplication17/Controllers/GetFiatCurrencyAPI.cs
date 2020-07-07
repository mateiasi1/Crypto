using AutoMapper;
using BusinessLayer;
using Data_Layer.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

using DataLayer.DTO;
using WebApplication17.Models;
using Microsoft.AspNetCore.Authorization;

namespace DataLayer.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class GetFiatCurrencyAPI : Controller
    {
        private readonly IMapper _mapper;
        public GetFiatCurrencyAPI(IMapper mapper)
        {
            _mapper = mapper;
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
                    return currency;
                }
                catch (HttpRequestException httpRequestException)
                {
                    return null;
                }
            }
        }

        [HttpGet("{from}/{to}")]
        public async Task<ActionResult<ConversionRate>> GetConversionRate(string from, string to)
        {
            List<ConversionRate> conversion = new List<ConversionRate>();
            using (var client = new HttpClient())
            {
                try
                {
                    client.BaseAddress = new Uri("https://min-api.cryptocompare.com/data/price?fsym=" + from + "&tsyms=" + to);
                    var response = await client.GetAsync(client.BaseAddress);

                    response.EnsureSuccessStatusCode();

                    var stringResult = await response.Content.ReadAsStringAsync();
                    var jsonData = JObject.Parse(stringResult);
                    foreach (var item in jsonData)
                    {
                        if (item.Key != to)
                        {
                            continue;
                        }

                        foreach (var items in jsonData)
                        {
                            var itemValueResult = items.Value.ToString();
                            ConversionRate conversionRate = new ConversionRate();
                            conversionRate.ConversionRateValue = Convert.ToDouble(itemValueResult);
                            conversion.Add(conversionRate);
                        }
                    }
                    return Ok(_mapper.Map<IEnumerable<ConversionRate>>(conversion));
                }
                catch (HttpRequestException httpRequestException)
                {
                    return BadRequest($"Error getting crypto: {httpRequestException.Message}");
                }
            }
        }
    }
}
