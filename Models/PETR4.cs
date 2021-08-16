using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Financa.Models
{
    [NotMapped]
    public class PETR4
    {
        public PETR4()
        {
        }

        public PETR4(string symbol, string name, string company_name, string document, string description, string website, string region, string currency, Market_Time market_time, double market_cap, double price, double change_percent, DateTime updated_at)
        {
            this.symbol = symbol;
            this.name = name;
            this.company_name = company_name;
            this.document = document;
            this.description = description;
            this.website = website;
            this.region = region;
            this.currency = currency;
            this.market_time = market_time;
            this.market_cap = market_cap;
            this.price = price;
            this.change_percent = change_percent;
            this.updated_at = updated_at;
        }
        [JsonProperty(PropertyName = "PETR4")]
        public PETR4 acao { get; set; }

        [JsonProperty(PropertyName = "symbol")]
        public string symbol { get; set; }
        [JsonProperty(PropertyName = "name")]
        public string name { get; set; }
        [JsonProperty(PropertyName = "company_name")]
        public string company_name { get; set; }
        [JsonProperty(PropertyName = "document")]
        public string document { get; set; }
        [JsonProperty(PropertyName = "description")]
        public string description { get; set; }
        [JsonProperty(PropertyName = "website")]
        public string website { get; set; }
        [JsonProperty(PropertyName = "region")]
        public string region { get; set; }
        [JsonProperty(PropertyName = "currency")]
        public string currency { get; set; }
        [JsonProperty(PropertyName = "market_time")]
        public Market_Time market_time { get; set; }
        [JsonProperty(PropertyName = "market_cap")]
        public double market_cap { get; set; }
        [JsonProperty(PropertyName = "price")]
        public double price { get; set; }
        [JsonProperty(PropertyName = "change_percent")]
        public double change_percent { get; set; }
        [JsonProperty(PropertyName = "updated_at")]
        public DateTime updated_at { get; set; }

    }
}
