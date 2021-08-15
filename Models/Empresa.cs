using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Financa.Models
{
    public class Empresa
    {
        public Empresa()
        {
        }

        public Empresa(int id, string ticker, string nome, EmpresaTipo empresaTipo)
        {
            Id = id;
            Ticker = ticker;
            Nome = nome;
            EmpresaTipo = empresaTipo;
        }

        public Empresa(int id, string ticker, string nome, EmpresaTipo empresaTipo, ICollection<Investimento> investimentos)
        {
            Id = id;
            Ticker = ticker;
            Nome = nome;
            EmpresaTipo = empresaTipo;
            Investimentos = investimentos;
        }

        public int Id { get; set; }
        [Required]
        public string Ticker { get; set; }
        [Required]
        public string Nome { get; set; }        
        public EmpresaTipo EmpresaTipo { get; set; }
        public virtual ICollection<Investimento> Investimentos { get; set; }
     
    }
    
}
