using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Financa.Models
{
    public class Provento
    {
        public Provento()
        {
        }

        public Provento(int id, DateTime dataFechamento, decimal valorCarteira, decimal totalRecebido, decimal porcentagemReferenteAoMes)
        {
            Id = id;
            DataFechamento = Convert.ToDateTime(DataFechamento).Date;
            ValorCarteira = valorCarteira;
            TotalRecebido = totalRecebido;
            PorcentagemReferenteAoMes = porcentagemReferenteAoMes;
        }

        public int Id { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [Display(Name="Data")]
        public DateTime DataFechamento { get; set; }        
        [Display(Name = "Valor da Carteira")]
        public decimal ValorCarteira { get; set; }
        [Display(Name = "Total Recebido")]
        public decimal TotalRecebido { get; set; }
        [Display(Name = "Porcentagem Referente ao mês")]
        public decimal PorcentagemReferenteAoMes { get; set; }
    }
}
