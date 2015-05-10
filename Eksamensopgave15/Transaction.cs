using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eksamensopgave15
{
    abstract class Transaction
    {
        protected int id;
        public User user { get; set; }
        protected DateTime date;
        public int price { get; set; }

        public Transaction(int id, User user, int price)
        {
            this.id = id;
            this.user = user;
            this.price = price;

            date = DateTime.Now;
        }

        public override string ToString()
        {
            return "Transaction ID: " + id + " Amount: " + price + " Date: " + date;
        }

        public abstract void Execute();
    }
}
