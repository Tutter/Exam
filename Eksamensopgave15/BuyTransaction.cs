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

        //Constructor for buying 1 product
        public BuyTransaction(int id, User user, Product product, int price) : base(id, user, price)
        {
            this.product = product;
        }

        //Constructor for buying multiple products
        public BuyTransaction(int id, User user, Product product, int price, int amount)
            : base(id, user, price * amount)
        {
            this.product = product;
        }

        public override string ToString()
        {
            return base.ToString() + "; Bought " + product.name + " from the user: " + user.username + " \n";
        }

        public override void Execute()
        {
            if (product.canBeBoughtOnCredit == true)
                user.balance -= price;
            else if(user.balance >= price)
                user.balance -= price;
            else
                throw new InsufficientCreditsException(user, product);
        }
    }
}
