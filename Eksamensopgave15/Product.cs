﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eksamensopgave15
{
    class Product
    {
        public int id { get; set; }
        public string name { get; set; }
        public int price { get; set; }
        public bool active { get; set; }
        public bool canBeBoughtOnCredit;

        public Product(int id, string name, int price, bool active)
        {
            this.id = id;
            this.name = name;
            this.price = price;
            this.active = active;
            canBeBoughtOnCredit = false;
        }

        public override string ToString()
        {
            return id + "\t" + name + "\t" + (float)price / 100;
        }
    }
}
