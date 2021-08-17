﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Financa.Models
{
    public class Acao
    {
        [Display(Name = "Valor Atual*")]
        public double Ask { get; set; }
        public double AverageVolume { get; set; }
        [Display(Name = "Preço de Venda")]
        public double Bid { get; set; }
        public double LastTradePrice { get; set; }
        public string NameCompany { get; set; }
        [Display(Name = "Mudança Percentual")]
        public double RegularMarketChangePercent { get; set; }
        public string ShortName { get; set; }
        public string Symbol { get; set; }
        public string Ticker { get; set; }
        public Exception erro { get; set; }
    }
}
