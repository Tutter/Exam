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

        public BuyTransaction(User user, Product product, int price) : base(user, price)
        {
            this.product = product;
            amountOfProduct = 1;
        }

        public BuyTransaction(User user, Product product, int price, int amount)
            : base(user, price)
        {
            this.product = product;
            amountOfProduct = amount;
        }

        public override string ToString()
        {
            return "Buying product from user: " + user + "'s account" + "\n" + base.ToString();
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
