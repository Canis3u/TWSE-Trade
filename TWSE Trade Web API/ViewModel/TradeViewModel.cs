﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TWSE_Trade_Web_API.ViewModel
{
    public class TradeViewModel
    {
        public string Type { get; set; }
        public int Volume { get; set; }
        public float Fee { get; set; }
        public int LendingPeriod { get; set; }
    }
}
