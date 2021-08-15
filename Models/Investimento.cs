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
        [NotMappedAttribute]
        public IFormFile FileUpload { get; set; }


    }
}
