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

        public StregsystemCLI(Stregsystem stregsystem)
        {
            this.stregsystem = stregsystem;
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

        public void DisplayNotValidProductID()
        {
            Console.WriteLine("That is not a valid product ID");
        }

        public void DisplayNotValidAmountOfProduct()
        {
            Console.WriteLine("That is not a valid product amount");
        }

        public void DisplayUserInfo(string userName)
        {
            Console.WriteLine(stregsystem.userList.GetUserByUserName(userName).ToString());
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

        public void DisplayInsertedCashToUser(string userName, int amount)
        {
            Console.WriteLine("Inserted " + amount + " ører into user: " + userName + "'s account");
        }

        public void Close()
        {
            Console.WriteLine("Closing the program");
        }

        public void DisplayInsufficientCash(string userName, string productName)
        {
            Console.WriteLine("User: " + userName + "has insufficient credits for " + productName);
        }

        public void DisplayGeneralError(string errorString)
        {
            Console.WriteLine("Error: " + errorString);
        }
    }
}
