using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eksamensopgave15
{
    class BuyTransaction : Transaction
    {
        private Product product;

        public BuyTransaction(User user, Product product, int amount) : base(user, amount)
        {
            this.product = product;
        }

        public override string ToString()
        {
            return "Buying product from user: " + user + "'s account" + "\n" + base.ToString();
        }

        public override void Execute()
        {
            if(user.balance >= amount)
                user.balance -= amount;
            else
                throw new InsufficientCreditsException(user, product);
        }
    }
}
