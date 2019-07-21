using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using WebApplication17.Data;
using WebApplication17.Models;

namespace WebApplication17.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class GetFiatCurrencyAPI : Controller
    {

        private readonly Contexts _context;

        public GetFiatCurrencyAPI(Contexts context)
        {
            _context = context;
        }
        // GET: /<controller>/
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Currency>>> GetCurrencyFromAPI()
        {
            List<Currency> currency;
            using (var client = new HttpClient())
            {
                try
                {
                    client.BaseAddress = new Uri("http://apilayer.net/api/live");
                    var response = await client.GetAsync($"?access_key=6a68fbb7153ff890b106018dd642c8bb");
                    response.EnsureSuccessStatusCode();

                    var stringResult = await response.Content.ReadAsStringAsync();
                    var jsonData = JObject.Parse(stringResult);
                    foreach (var item in jsonData)
                    {
                        //aici o sa fie afisarea in pagina
                    }
                    return Ok();
                }
                catch (HttpRequestException httpRequestException)
                {
                    return BadRequest($"Error getting crypto: {httpRequestException.Message}");
                }
            }
        }

    }
}
