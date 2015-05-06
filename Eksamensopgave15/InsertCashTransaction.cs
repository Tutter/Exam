using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eksamensopgave15
{
    class InsertCashTransaction : Transaction
    {
        public override string ToString()
        {
            return "Inserting cash to user: " + user + "'s balance" + "\n" + base.ToString();
        }

        public override void Execute()
        {
            user.balance += amount;
        }
    }
}
