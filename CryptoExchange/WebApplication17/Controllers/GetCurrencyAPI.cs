using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebApplication17.Models;

namespace WebApplication17.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GetCurrencyAPI : Controller
    {
        // GET: /<controller>/
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Currency>>> GetCurrencyFromAPI()
        {
            using (var client = new HttpClient())
            {
                try
                {
                    client.BaseAddress = new Uri("https://min-api.cryptocompare.com/data/all/coinlist");
                    var response = await client.GetAsync($"/getPrice?2149e65731f02e67abf4fd485d77e24a88931fe884fb974841718fd3a37b4e38");
                    response.EnsureSuccessStatusCode();

                    var stringResult = await response.Content.ReadAsStringAsync();
                    var rawCrypto = JsonConvert.DeserializeObject<Crypto>(stringResult);
                    return Ok(new
                    {
                        Name = rawCrypto.Name,
                        Price = string.Join(":", rawCrypto.Name)
                    });
                }
                catch (HttpRequestException httpRequestException)
                {
                    return BadRequest($"Error getting crypto: {httpRequestException.Message}");
                }
            }
        }
    }
}
