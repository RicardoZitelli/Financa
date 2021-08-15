using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Financa.Models
{
    public class EmpresaTipo
    {
        public EmpresaTipo()
        {
        }

        public EmpresaTipo(int id, string descricao)
        {
            Id = id;
            Descricao = descricao;
        }

        public EmpresaTipo(int id, string descricao, ICollection<Empresa> empresas)
        {
            Id = id;
            Descricao = descricao;
            Empresas = empresas;
        }

        public int Id { get; set; }
        [Display(Name ="Descrição")]
        public string Descricao { get; set; }
        public virtual ICollection<Empresa> Empresas  { get; set; }

    }
}
