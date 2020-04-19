using Data_Layer.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Net;
using System.Net.Http;

namespace BusinessLayer
{
    public class GetConversionRateAsync
    {
        public double GetConversionRate(string from, string to)
        {
            double conversion = 0;
            var baseAddress = new Uri("https://min-api.cryptocompare.com/data/price?fsym=" + from + "&tsyms=" + to);
            using (var wb = new WebClient())
            {
                var response = wb.DownloadString(baseAddress);
                var jsonData = JObject.Parse(response);
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
                        conversion = conversionRate.ConversionRateValue;
                    }
                }
                return conversion;
            }
        }

    }
}