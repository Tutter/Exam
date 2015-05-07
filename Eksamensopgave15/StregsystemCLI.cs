using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eksamensopgave15
{
    class StregsystemCLI : IStregsystemUI
    {
        public Stregsystem stregsystem;

        public StregsystemCLI()
        {
            stregsystem = new Stregsystem();
        }

        public void Start()
        {
            DisplayProducts();
        }

        private void DisplayProducts()
        {
            foreach (Product product in stregsystem.GetActiveProducts())
            {
                Console.WriteLine(product.ToString());
            }
        }

        public void DisplayUserNotFound(string userName)
        {
            Console.WriteLine("The user: " + userName + " was not found");
        }

        public void DisplayProductNotFound(int productId)
        {
            Console.WriteLine("The product with ID: " + productId + " was not found");
        }

        public void DisplayUserInfo(string userName)
        {
            Console.WriteLine(stregsystem.userList.GetUserByUserName(userName));
        }

        public void DisplayTooManyArgumentsError()
        {
            Console.WriteLine("Too many arguments given");
        }

        public void DisplayAdminCommandNotFoundMessage()
        {
            Console.WriteLine("That is not a valid admin command");
        }

        public void DisplayUserBuysProduct(BuyTransaction transaction)
        {
            Console.WriteLine("User: " + transaction.user.userName + " bought " + transaction.product.name);
        }

        public void DisplayUserBuysProduct(BuyTransaction transaction, int amount)
        {
            Console.WriteLine("User: " + transaction.user.userName + " bought " + amount + " of " + transaction.product.name);
        }

        public void Close()
        {
            Console.WriteLine("Closing the program");
        }

        public void DisplayInsufficientCash()
        {
            Console.WriteLine("User");
        }

        public void DisplayGeneralError(string errorString)
        {
            Console.WriteLine("Error: " + errorString);
        }
    }
}
