using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using AutoMapper;
using Data_Layer.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using DataLayer.DTO;
using WebApplication17.Models;

namespace DataLayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GetCryptoCurrencyAPI : CustomBaseController
    {
        private readonly IMapper _mapper;

        public GetCryptoCurrencyAPI(IMapper mapper)
        {
            _mapper = mapper;
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
                    
                        var request = "https://min-api.cryptocompare.com/data/all/coinlist?access_key=6a68fbb7153ff890b106018dd642c8bb";
                        var response = client.GetAsync(request).Result;
                        var content = response.Content.ReadAsStringAsync().Result;
                    

                    JObject field = JsonConvert.DeserializeObject<JObject>(content);
                    JObject fieldData = JsonConvert.DeserializeObject<JObject>(field["Data"].ToString());
                    var content1 = fieldData.ToString();

                    JObject results = JsonConvert.DeserializeObject<JObject>(content1);
                    foreach (KeyValuePair<string, JToken> item in results)
                    {
                        CryptoCurrency cryptoCurrency = new CryptoCurrency();
                        var symbol = item.Value["Symbol"].ToString();
                        var fullName = item.Value["FullName"].ToString();
                        cryptoCurrency.CryptoCurrencyAbbreviation = symbol;
                        cryptoCurrency.CryptoCurrencyName = fullName;
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
