using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Financa.Models;
using Newtonsoft.Json;
using System.Net;
using YahooFinanceApi;

namespace Financa.Controllers
{
    public class CotacaoBolsaController : Controller
    {
        public async Task<IActionResult> Index()
        {
            var awaiter = getStockData();


            return View();
        }

        public async Task<int> getStockData()
        {
            try
            {
                string symbol = Request.Query["symbol"];
                string type = Request.Query["type"].ToString().ToUpper();
                type = type == "FII" ? ".SA" : "";
                string param = symbol + type;
                var security = await Yahoo.Symbols(symbol+".SA").QueryAsync();
                var securities = security.Values.ToList();
                double preco;
               
                foreach(var item in securities)
                {
                    preco = item.Ask;
                }
                
            }
            catch (Exception ex)
            {


            }

            return 1;
        }

        public ContentResult Acao(string symbol)
        {
            string strURL = "https://api.hgbrasil.com/finance/stock_price?key=40331baa&symbol=" + symbol;

            JsonSerializerSettings _jsonSetting = new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore };
            string result = "";

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
