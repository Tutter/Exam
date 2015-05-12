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
        private bool running;

        public StregsystemCLI(Stregsystem stregsystem)
        {
            this.stregsystem = stregsystem;
            running = true;
        }

        //Starts the command interface
        public void Start(StregsystemCommandParser commandParser)
        {
            MainLoop(commandParser);
        }

        //The main loop of the program that takes commands and prints out to the user
        private void MainLoop(StregsystemCommandParser commandParser)
        {
            while (running)
            {
                Console.Clear();
                DisplayProducts();
                Console.Write("Enter a command: ");
                commandParser.ParseCommand();
                Console.ReadKey();
            }
        }

        //Displays the active products
        private void DisplayProducts()
        {
            foreach (Product product in stregsystem.GetActiveProducts())
            {
                Console.WriteLine(product.ToString());
            }
            Console.WriteLine();
        }

        public void DisplayUserNotFound(string username)
        {
            Console.WriteLine("The user: " + username + " was not found");
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

        //Displays information about a user by username
        public void DisplayUserInfo(string username)
        {
            User user = stregsystem.userList.GetUserByUsername(username);
            Console.WriteLine(user.ToString() + "\nUser's balance: " + ((float)user.balance/100) + "kr");
            DisplayBalanceUnderFifty(user);
            DisplayUserLastTenTransactions(user);
        }

        //Displays a message if the user has a balance under 50kr
        public void DisplayBalanceUnderFifty(User user)
        {
            if (user.balance < 5000)
                Console.WriteLine("User's balance is under 50kr");
        }

        public void DisplayTooManyArgumentsError()
        {
            Console.WriteLine("Too many arguments given");
        }

        public void DisplayAdminCommandNotFoundMessage()
        {
            Console.WriteLine("That is not a valid admin command");
        }

        //Overloaded method that displays a message when a user buys a single product
        public void DisplayUserBuysProduct(BuyTransaction transaction)
        {
            Console.WriteLine("User: " + transaction.user.username + " bought " + transaction.product.name);
        }

        //Overlaoded method that displays a message when a user buys multiple products
        public void DisplayUserBuysProduct(BuyTransaction transaction, int amount)
        {
            Console.WriteLine("User: " + transaction.user.username + " bought " + amount + " of " + transaction.product.name);
        }

        public void DisplayInsertedCashToUser(string username, int amount)
        {
            Console.WriteLine("Inserted " + amount + " kr into user: " + username + "'s account");
        }

        public void DisplayNotValidCreditAmount()
        {
            Console.WriteLine("Invalid amount of credits to transfer");
        }

        //Displays a closing message and set the main loops sentinel value to false
        public void Close()
        {
            Console.WriteLine("Closing the program");
            running = false;
        }

        public void DisplayInsufficientCash(string username, string productName)
        {
            Console.WriteLine("User: " + username + " has insufficient credits for " + productName);
        }

        public void DisplayGeneralError(string errorString)
        {
            Console.WriteLine("Error: " + errorString);
        }

        //Displays a given users latest ten transactions
        private void DisplayUserLastTenTransactions(User user)
        {
            List<string[]> transactions = stregsystem.GetTransactionList(10, user.username);

            foreach (string[] str in transactions)
            {
                Console.WriteLine(str[0]);
                Console.WriteLine(str[1]);
            }
        }
    }
}
