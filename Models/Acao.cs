using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Financa.Models
{
    public class Acao
    {
        public int String { get; set; }
        public decimal AverageVolume { get; set; }
        public decimal LastTradePrice { get; set; }
        public decimal Bid { get; set; }
        public decimal Ask { get; set; }
    }
}
