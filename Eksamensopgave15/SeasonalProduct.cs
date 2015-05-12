using System;
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

        //Constructor that takes to dates and assigns them
        public SeasonalProduct(int id, string name, int price, DateTime seasonStartDate, DateTime seasonEndDate)
            : base(id, name, price, false)
        {
            this.seasonStartDate = seasonStartDate;
            this.seasonEndDate = seasonEndDate;

            if ((seasonStartDate < DateTime.Now || seasonStartDate == null) && (seasonEndDate > DateTime.Now || seasonEndDate == null))
            {
                this.active = true;
            }
        }
    }
}
