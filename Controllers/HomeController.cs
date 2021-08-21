using Financa.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Financa.Data;
using System.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Runtime.Serialization;

namespace Financa.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly string stringConnection;
        JsonSerializerSettings _jsonSetting = new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore };

        [DataContract]
        struct Struct_sp_TotalInvestidoEmpresa
        {
            public Struct_sp_TotalInvestidoEmpresa(string label, double y)
            {
                Label = label;
                Y = y;
            }

            [DataMember(Name = "label")]
            public string Label { get; set; }
            [DataMember(Name = "y")]
            public double Y { get; set; }
        }

        public HomeController(ILogger<HomeController> logger)
        {            
            _logger = logger;
            stringConnection = "Server=DESKTOP-UT5R5SE;Database=Financa;Trusted_Connection=True;MultipleActiveResultSets=true";
        }

        public IActionResult Index()
        {
            TotalInvestidoPorEmpresa();

            QuantidadeAcoesInvestidoPorEmpresa();

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public void TotalInvestidoPorEmpresa()
        {
            using (SqlConnection sql = new SqlConnection(stringConnection))
            {
                using (SqlCommand cmd = new SqlCommand("sp_TotalInvestidoEmpresa", sql))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                  
                    sql.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {                        
                        List<Struct_sp_TotalInvestidoEmpresa> dataPoint = new List<Struct_sp_TotalInvestidoEmpresa>();

                        while (reader.Read())
                        {
                            dataPoint.Add(new Struct_sp_TotalInvestidoEmpresa(reader[0].ToString(), double.Parse(reader[1].ToString())));
                        }
                        
                        ViewBag.TotalPorEmpresa = JsonConvert.SerializeObject(dataPoint);                        
                    }
                }
            }
        }

        public void QuantidadeAcoesInvestidoPorEmpresa()
        {
            using (SqlConnection sql = new SqlConnection(stringConnection))
            {
                using (SqlCommand cmd = new SqlCommand("sp_TotalInvestidoEmpresa", sql))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    sql.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        List<Struct_sp_TotalInvestidoEmpresa> dataPoint = new List<Struct_sp_TotalInvestidoEmpresa>();

                        while (reader.Read())
                        {
                            dataPoint.Add(new Struct_sp_TotalInvestidoEmpresa(reader[0].ToString(), double.Parse(reader[2].ToString())));
                        }

                        ViewBag.QuantidadeAcoes = JsonConvert.SerializeObject(dataPoint);
                    }
                }
            }
        }
    }
}
