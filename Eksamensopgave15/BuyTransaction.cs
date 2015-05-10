using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eksamensopgave15
{
    class BuyTransaction : Transaction
    {
        public Product product { get; set; }

        public BuyTransaction(int id, User user, Product product, int price) : base(id, user, price)
        {
            this.product = product;
        }

        public BuyTransaction(int id, User user, Product product, int price, int amount)
            : base(id, user, price * amount)
        {
            this.product = product;
        }

        public override string ToString()
        {
            return base.ToString() + " Bought " + product.name + " from user: " + user.userName + "'s account" + "\n";
        }

        public override void Execute()
        {
            if(user.balance >= price)
                user.balance -= price;
            else
                throw new InsufficientCreditsException(user, product);
        }
    }
}
