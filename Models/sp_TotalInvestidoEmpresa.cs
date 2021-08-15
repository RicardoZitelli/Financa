using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Financa.Models
{
    public partial class sp_TotalInvestidoEmpresa
    {
        public string Empresa { get; set; }
        public decimal Total_Investido { get; set; }
        public int Quantidade { get; set; }
        public decimal Porcentagem { get; set; }
        public decimal TotalFinal { get; set; }
    }
}
