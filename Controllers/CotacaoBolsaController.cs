using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Financa.Models;
using Newtonsoft.Json;


namespace Financa.Controllers
{
    public class CotacaoBolsaController : Controller
    {
        public IActionResult Index()
        {
            
            return View();
        }

        public ContentResult Acao(string symbol)
        {
            string strURL = "https://api.hgbrasil.com/finance/stock_price?key=40331baa&symbol=" + symbol;

            JsonSerializerSettings _jsonSetting = new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore };
            string result="";
            
            using (HttpClient client = new HttpClient())
            {
                var response = client.GetAsync(strURL).Result;

                if (response.IsSuccessStatusCode)
                {
                    result = response.Content.ReadAsStringAsync().Result;

                    return Content(JsonConvert.SerializeObject(result, _jsonSetting), "application/json");

                }

                return Content(JsonConvert.SerializeObject(result, _jsonSetting), "application/json");
            }
        }
    }

   
}
