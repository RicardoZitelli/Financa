using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Financa.Models
{
    [DataContract]
    public class DataPoints
    {
        public DataPoints(string label, double y)
        {
            Label = label;
            Y = y;
        }

        [DataMember(Name = "label")]
        public string Label { get; set; }
        [DataMember(Name = "y")]
        public double Y { get; set; }
    }
}
