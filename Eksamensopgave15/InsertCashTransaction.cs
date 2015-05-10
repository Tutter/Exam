using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eksamensopgave15
{
    class InsertCashTransaction : Transaction
    {
        public InsertCashTransaction(int id, User user, int amount) 
            : base(id, user, amount)
        {

        }

        public override string ToString()
        {
            return base.ToString() + " Inserted cash to user: " + user.userName + "'s balance";
        }

        public override void Execute()
        {
            user.balance += price;
        }
    }
}
