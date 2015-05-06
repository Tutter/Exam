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
        protected User user;
        protected DateTime date;
        protected int amount;

        public Transaction(int id, User user, DateTime date, int amount)
        {
            this.id = id;
            this.user = user;
            this.date = date;
            this.amount = amount;
        }

        public override string ToString()
        {
            return "Transaction ID: " + id + "\nAmount: " + amount + "\nDate: " + date;
        }

        public abstract void Execute();
    }
}
