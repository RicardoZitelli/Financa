using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Financa.Models
{
    public class Mercado
    {      
        public Mercado(Result result)
        {
            this.Result = result;
        }

        [JsonProperty(PropertyName = "results")]
        public Result Result{ get; set; }

        [JsonProperty(PropertyName = "by")]
        public string By { get; set; }

        [JsonProperty(PropertyName = "valid_key")]
        public bool Valid_key { get; set; }
                
    }

   
}
