using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Financa.Models
{
    [NotMapped]
    public class Market_Time
    {
        public Market_Time(TimeSpan open, TimeSpan close, DateTime timezone)
        {
            this.open = open;
            this.close = close;
            this.timezone = timezone;
        }

        [JsonProperty(PropertyName = "open")]
        public TimeSpan open { get; set; }

        [JsonProperty(PropertyName = "close")]
        public TimeSpan close { get; set; }

        [JsonProperty(PropertyName = "timezone")]
        public DateTime timezone { get; set; }
    }
}
