﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eksamensopgave15
{
    class SeasonalProduct : Product
    {
        private DateTime seasonStartDate;
        private DateTime seasonEndDate;

        public SeasonalProduct(int id, string name, int price, bool active, DateTime seasonStartDate)
            : base(id, name, price, active)
        {
            this.seasonStartDate = seasonStartDate;
        }

        public SeasonalProduct(int id, string name, int price, bool active, DateTime seasonEndDate)
            : base(id, name, price, active)
        {
            this.seasonEndDate = seasonEndDate;
        }

        public SeasonalProduct(int id, string name, int price, bool active, DateTime seasonStartDate, DateTime seasonEndDate)
            : base(id, name, price, active)
        {
            this.seasonStartDate = seasonStartDate;
            this.seasonEndDate = seasonEndDate;
        }
    }
}