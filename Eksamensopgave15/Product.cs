using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eksamensopgave15
{
    class Product
    {
        private int id;
        public string name { get; set; }
        public int price { get; set; }
        private bool active;
        private bool canBeBoughtOnCredit;

        public Product(int id, string name, int price, bool active)
        {
            this.id = id;
            this.name = name;
            this.price = price;
            this.active = active;
            canBeBoughtOnCredit = false;
        }
    }
}
