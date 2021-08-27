using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Financa.Models
{
    [NotMapped]
    public class vw_Investimentos
    {
        public vw_Investimentos()
        {
        }

        public vw_Investimentos(int id, DateTime data, string ticker, string tipo, int quantidade, decimal precoCompra, decimal valor_Total, double porcentagem, decimal corretagem, decimal valor_Total_Investimento, string userId)
        {
            Id = id;
            Data = data;
            Ticker = ticker;
            Tipo = tipo;
            Quantidade = quantidade;
            PrecoCompra = precoCompra;
            Valor_Total = valor_Total;
            Porcentagem = porcentagem;
            Corretagem = corretagem;
            Valor_Total_Investimento = valor_Total_Investimento;
            UserId = userId;
        }

        public int Id { get; set; }
        public DateTime Data { get; set; }
        public string Ticker { get; set; }
        public string Tipo { get; set; }
        public int Quantidade { get; set; }
        public decimal PrecoCompra { get; set; }
        public decimal Valor_Total { get; set; }
        public double Porcentagem { get; set; }
        public decimal Corretagem { get; set; }
        public decimal Valor_Total_Investimento { get; set; }
        public string UserId { get; set; }
    }
}
