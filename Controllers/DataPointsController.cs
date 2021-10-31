using Financa.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Data;

namespace Financa.Controllers
{
    public class DataPointsController : Controller
    {                      
        private readonly string stringConnection;
        JsonSerializerSettings _jsonSetting = new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore };
        public DataPointsController(IConfiguration config)
        {         
            stringConnection = config.GetConnectionString("DefaultConnection");
            
        }

        public IActionResult Index()
        {
            if (!(User.Identity.Name is null))
            {
                TotalInvestidoPorEmpresa();

                QuantidadeAcoesInvestidoPorEmpresa();
            }

            return View();
        }
        public void TotalInvestidoPorEmpresa()
        {
            using (SqlConnection sql = new SqlConnection(stringConnection))
            {
                using (SqlCommand cmd = new SqlCommand("sp_TotalInvestidoEmpresa", sql))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@UserName", User.Identity.Name);

                    sql.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        List<DataPoints> dataPoint = new List<DataPoints>();

                        while (reader.Read())
                        {
                            dataPoint.Add(new DataPoints(reader[0].ToString(), double.Parse(reader[1].ToString())));
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

                    cmd.Parameters.AddWithValue("@UserName", User.Identity.Name);

                    sql.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        List<DataPoints> dataPoint = new List<DataPoints>();

                        while (reader.Read())
                        {
                            dataPoint.Add(new DataPoints(reader[0].ToString(), double.Parse(reader[2].ToString())));
                        }

                        ViewBag.QuantidadeAcoes = JsonConvert.SerializeObject(dataPoint);
                    }
                }
            }
        }
    }
}
