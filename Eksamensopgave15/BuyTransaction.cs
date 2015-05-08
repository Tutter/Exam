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
        public int amountOfProduct { get; set; }

        public BuyTransaction(int id, User user, Product product, int price) : base(id, user, price)
        {
            this.product = product;
            amountOfProduct = 1;
        }

        public BuyTransaction(int id, User user, Product product, int price, int amount)
            : base(id, user, price)
        {
            this.product = product;
            amountOfProduct = amount;
        }

        public override string ToString()
        {
            return base.ToString() + "Bought " + product.name + " from user: " + user.userName + "'s account" + "\n";
        }

        public override void Execute()
        {
            if(user.balance >= price * amountOfProduct)
                user.balance -= price * amountOfProduct;
            else
                throw new InsufficientCreditsException(user, product);
        }
    }
}
