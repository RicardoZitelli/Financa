using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Financa.Models
{
    [NotMapped]
    public class Result
    {
        public Result(PETR4 acao)
        {
            this.Acao = acao;
        }
        
        [JsonProperty(PropertyName ="PETR4")]
        public PETR4 Acao { get; set; }

    }
}
