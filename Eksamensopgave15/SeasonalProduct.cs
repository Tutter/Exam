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

        //Constuctor that takes one date and checks if its an end or start date and then assigns it correctly
        public SeasonalProduct(int id, string name, int price, bool active, DateTime date)
            : base(id, name, price, active)
        {
            if (date > DateTime.Now)
            {
                seasonEndDate = date;
            }
            else
            {
                seasonStartDate = date;
            }
        }

        //Constructor that takes to dates and assigns them.
        public SeasonalProduct(int id, string name, int price, bool active, DateTime seasonStartDate, DateTime seasonEndDate)
            : base(id, name, price, active)
        {
            this.seasonStartDate = seasonStartDate;
            this.seasonEndDate = seasonEndDate;
        }
    }
}
