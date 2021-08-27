using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Financa.Models
{
    public class Investimento
    {
        public Investimento()
        {
        }

        public Investimento(int id, DateTime data, string tipo, int quantidade, decimal precoCompra, decimal? precoVenda, decimal? corretagem, DateTime? dataVenda, Corretora corretora, Empresa empresa)
        {
            Id = id;
            Data = Convert.ToDateTime(data).Date;
            Tipo = tipo;
            Quantidade = quantidade;
            PrecoCompra = precoCompra;
            PrecoVenda = precoVenda;
            Corretagem = corretagem;
            DataVenda ??= Convert.ToDateTime(dataVenda).Date;
            Corretora = corretora;
            Empresa = empresa;
        }

        public int Id { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Data da Compra")]
        [Required]
        public DateTime Data { get; set; }
        [Required]
        public string Tipo { get; set; }
        [Required]
        public int Quantidade { get; set; }
        [Display(Name = "Preço de Compra")]
        [Required]
        public decimal PrecoCompra { get; set; }
        [NotMapped]
        [Display(Name ="Valor Total")]
        public decimal Valor_Total { get; set; }
        [Display(Name ="Preço da Venda")]
        public decimal? PrecoVenda { get; set; }        
        public decimal? Corretagem { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Data da Venda")]
        public DateTime? DataVenda { get; set; }
        [Required]
        [Display(Name = "Corretora")]
        public int? CorretoraId { get; set; }
        [Required]
        [Display(Name = "Empresa")]
        public int? EmpresaId { get; set; }        
        public Corretora Corretora { get; set; }
        public Empresa Empresa { get; set; }
        public string UserId { get; set; }
        [NotMappedAttribute]
        public IFormFile FileUpload { get; set; }
        [NotMapped]
        public Acao Acao { get; set; }
        [NotMapped]
        public double Porcentagem {get; set; }
        [NotMapped]
        [Display(Name ="Valor da Carteira")]
        public double ValorCarteira { get; set; }
        [NotMapped]
        [Display(Name ="Valorização")]
        public double Valorizacao { get; set; }
        [NotMapped]
        [Display(Name = "Valorização(%)")]
        public double ValorizacaoPercentual { get; set; }
        [NotMapped]
        [Display(Name = "Preço de Compra Médio")]
        public decimal PrecoCompraMedio { get; set; }
        [NotMapped]
        [Display(Name = "Performance Frente ao PM")]
        public double PerformanceFrenteAoPrecoMedio { get; set; }
        [NotMapped]
        public double Valor_Total_Porcentagem { get; set; }
        [NotMapped]
        [Display(Name = "Valor Total Investido")]
        public decimal Valor_Total_Investido { get; set; }
        [NotMapped]
        public decimal Valor_Total_Corretagem { get; set; }
        [NotMapped]        
        public int Quantidade_Registro_Por_Acao { get; set; }
        [NotMapped]
        public int Quantidade_Registro_Total { get; set; }
        [NotMapped]
        public int Quantidade_Acao_Total { get; set; }
        [NotMapped]        
        public decimal Valor_Total_Investido_Atual { get; set; }
        [NotMapped]
        public decimal Valor_Total_Valorizacao { get; set; }
        [NotMapped]
        public double Valor_Total_ValorizacaoPorcentual { get; set; }
        [NotMapped]        
        public double Valor_Total_LucroPrejuizoReferentePrecoMedio { get; set; }
        [NotMapped]
        public bool EhOPrimeiroRegistroDaEmpresa { get; set; }

    }
}
