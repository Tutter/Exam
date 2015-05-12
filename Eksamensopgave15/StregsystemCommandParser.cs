using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eksamensopgave15
{
    class StregsystemCommandParser
    {   
        public Stregsystem stregsystem;
        public StregsystemCLI stregsystemCLI;
        private Dictionary<string, Delegate> adminCommands;

        public StregsystemCommandParser(Stregsystem stregsystem, StregsystemCLI stregsystemCLI)
        {
            this.stregsystem = stregsystem;
            this.stregsystemCLI = stregsystemCLI;
            adminCommands = new Dictionary<string,Delegate>();
            FillAdminDictionary();
        }

        //Handles commands
        public void ParseCommand()
        {
            string[] currentCommand;
            int numOfArguments;

            currentCommand = GetUserInput();
            numOfArguments = currentCommand.Count();
            
            //If the command has the character ':' in it treat it as an admin command
            if (currentCommand[0].Contains(':'))
            {
                HandleAdminCommand(currentCommand, numOfArguments);
                return;
            }

            //Switch based on how many strings are in currentCommand
            switch (numOfArguments)
            {
                case 1:
                    UserInfoCommand(currentCommand);
                    break;
                case 2:
                    BuyProductCommand(currentCommand);
                    break;   
                case 3:
                    BuyMultipleProductCommand(currentCommand);
                    break;
                default:
                    stregsystemCLI.DisplayTooManyArgumentsError();
                    break;
            }
        }

        //Fills the admin dictionary with key/value pairs
        private void FillAdminDictionary()
        {
            adminCommands.Add(":quit", new Action(stregsystemCLI.Close));
            adminCommands.Add(":q", new Action(stregsystemCLI.Close));
            adminCommands.Add(":activate", new Action<int>(stregsystem.ActivateProduct));
            adminCommands.Add(":deactivate", new Action<int>(stregsystem.DeactivateProduct));
            adminCommands.Add(":crediton", new Action<int>(stregsystem.ActivateBuyOnCredit));
            adminCommands.Add(":creditoff", new Action<int>(stregsystem.DeactivateBuyOnCredit));
            adminCommands.Add(":addcredits", new Action<string, int>(InsertCashAdminCommand));
        }

        //Handles admin commands
        private void HandleAdminCommand(string[] currentCommand, int numOfArguments)
        {
            int productId = 0;
            int amountOfCredits = 0;

            //If the dictionary does not contain the given string as a key then display error and return
            if(!adminCommands.ContainsKey(currentCommand[0]))
            {
                stregsystemCLI.DisplayAdminCommandNotFoundMessage();
                return;
            }

            try
            {
                //Switch based on how many strings are in currentCommand
                switch (numOfArguments)
                {
                    //If numOfArguments is 1 then the command can only be a quit command
                    case 1:
                        adminCommands[currentCommand[0]].DynamicInvoke();
                        break;
                    //If numOfArguments is 2 then the command either activating or deactivating certain properties of a product
                    case 2:
                        Int32.TryParse(currentCommand[1], out productId);
                        
                        //If the product id is less than one then display not valid product id
                        if (productId < 1)
                        {
                            stregsystemCLI.DisplayNotValidProductID();
                        }
                        else
                        {
                            adminCommands[currentCommand[0]].DynamicInvoke(productId);
                        }
                        break;
                    //If numOfArguments is 3 then the command can only be an insert credit command
                    case 3:
                        Int32.TryParse(currentCommand[2], out amountOfCredits);

                        //If the amount of credits is less than 1 then display not valid amount of credits
                        if (amountOfCredits < 1)
                        {
                            stregsystemCLI.DisplayNotValidCreditAmount();
                        }
                        else
                        {
                            adminCommands[currentCommand[0]].DynamicInvoke(currentCommand[1], amountOfCredits);
                        }
                        break;
                    default:
                        stregsystemCLI.DisplayAdminCommandNotFoundMessage();
                        break;
                }         
            }
            //Catches all exceptions in the admin command handling and displays admin command not found if it does
            catch
            {
                stregsystemCLI.DisplayAdminCommandNotFoundMessage();
            }       
        }

        //Inserts credits to a users account
        private void InsertCashAdminCommand(string username, int amount)
        {
            User user;
            
            try
            {
                user = stregsystem.GetUser(username);
            }
            catch (UserNotFoundException e)
            {
                stregsystemCLI.DisplayUserNotFound(e.username);
                return;
            }

            stregsystem.AddCreditsToAccount(user, amount * 100);
            stregsystemCLI.DisplayInsertedCashToUser(user.username, amount);
        }

        //Gets user input
        private string[] GetUserInput()
        {
            return Console.ReadLine().Split(' ');
        }

        //Displays information about a given user
        private void UserInfoCommand(string[] currentCommand)
        {
            try
            {
                stregsystemCLI.DisplayUserInfo(currentCommand[0]);
            }
            catch (UserNotFoundException e)
            {
                stregsystemCLI.DisplayUserNotFound(e.username);
            }
        }

        //Buys a single product from a users account
        private void BuyProductCommand(string[] currentCommand)
        {
            User user;
            int productId = 0;
            Product product;
            BuyTransaction transaction;

            Int32.TryParse(currentCommand[1], out productId);
            
            //If the product id is less than one then display not valid product id
            if (productId < 1)
            {
                stregsystemCLI.DisplayNotValidProductID();
                return;
            }
            
            try
            {
                user = stregsystem.userList.GetUserByUsername(currentCommand[0]);
                product = stregsystem.productList.GetProductByID(productId);
                transaction = stregsystem.BuyProduct(user, product, product.price);
            }

            catch (UserNotFoundException e)
            {
                stregsystemCLI.DisplayUserNotFound(e.username);
                return;
            }

            catch (ProductNotFoundException e)
            {
                stregsystemCLI.DisplayProductNotFound(e.id);
                return;
            }

            catch (InsufficientCreditsException e)
            {
                stregsystemCLI.DisplayInsufficientCash(e.user.username, e.product.name);
                return;
            }

            stregsystemCLI.DisplayUserBuysProduct(transaction);
            if (user.balance < 50000)
            {
                stregsystemCLI.DisplayBalanceUnderFifty(user);
            }
        }

        //Buys multiple products from a users account
        private void BuyMultipleProductCommand(string[] currentCommand)
        {
            User user;
            int productId = 0;
            int amount = 0;
            Product product;
            BuyTransaction transaction;

            Int32.TryParse(currentCommand[1], out amount);
            //If the amount of credits is less than 1 then display not valid amount of credits
            if (amount < 1)
            {
                stregsystemCLI.DisplayNotValidAmountOfProduct();
                return;
            }

            Int32.TryParse(currentCommand[2], out productId);
            //If the product id is less than one then display not valid product id
            if (productId < 1)
            {
                stregsystemCLI.DisplayNotValidProductID();
                return;
            }

            try
            {
                user = stregsystem.userList.GetUserByUsername(currentCommand[0]);
                product = stregsystem.productList.GetProductByID(productId);
                transaction = stregsystem.BuyProduct(user, product, product.price, amount);
            }

            catch (UserNotFoundException e)
            {
                stregsystemCLI.DisplayUserNotFound(e.username);
                return;
            }

            catch (ProductNotFoundException e)
            {
                stregsystemCLI.DisplayProductNotFound(e.id);
                return;
            }

            catch (InsufficientCreditsException e)
            {
                stregsystemCLI.DisplayInsufficientCash(e.user.username, e.product.name);
                return;
            }

            stregsystemCLI.DisplayUserBuysProduct(transaction, amount);
            if (user.balance < 50000)
            {
                stregsystemCLI.DisplayBalanceUnderFifty(user);
            }
        }
    }
}
