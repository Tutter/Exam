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

        public void Start(StregsystemCommandParser commandParser)
        {
            MainLoop(commandParser);
        }

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

        private void DisplayProducts()
        {
            foreach (Product product in stregsystem.GetActiveProducts())
            {
                Console.WriteLine(product.ToString());
            }
            Console.WriteLine();
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
            User user = stregsystem.userList.GetUserByUserName(userName);
            Console.WriteLine(user.ToString() + "\nUser's balance: " + ((float)user.balance/100) + "kr");
            DisplayBalanceUnderFifty(user);
            DisplayUserLastTenTransactions(user);
        }

        private void DisplayBalanceUnderFifty(User user)
        {
            if (user.balance < 5000)
                Console.WriteLine(user.userName + "'s balance is under 50kr");
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
            Console.WriteLine("Inserted " + amount + " kr into user: " + userName + "'s account");
        }

        public void DisplayNotValidCreditAmount()
        {
            Console.WriteLine("Invalid amount of credits to transfer");
        }

        public void Close()
        {
            Console.WriteLine("Closing the program");
            running = false;
        }

        public void DisplayInsufficientCash(string userName, string productName)
        {
            Console.WriteLine("User: " + userName + " has insufficient credits for " + productName);
        }

        public void DisplayGeneralError(string errorString)
        {
            Console.WriteLine("Error: " + errorString);
        }

        private void DisplayUserLastTenTransactions(User user)
        {
            List<string[]> transactions = stregsystem.GetTransactionList(10, user.userName);

            foreach (string[] str in transactions)
            {
                Console.WriteLine(str[0]);
                Console.WriteLine(str[1]);
            }
        }
    }
}
