using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Financa.Models
{
    public class Corretora
    {
        public Corretora()
        {
        }

        public Corretora(int id, string descricao)
        {
            Id = id;
            Descricao = descricao;
        }

        public Corretora(int id, string descricao, ICollection<Empresa> empresas, ICollection<Investimento> investimentos)
        {
            Id = id;
            Descricao = descricao;            
            Investimentos = investimentos;
        }

        public int Id { get; set; }
        [Display(Name = "Descrição")]
        [Required]
        public string Descricao { get; set; }
        public virtual ICollection<Investimento> Investimentos { get; set; }
    }
}
