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

        public void ParseCommand()
        {
            string[] currentCommand;
            int numOfArguments;

            currentCommand = GetUserInput();
            numOfArguments = currentCommand.Count();
            
            if (currentCommand[0].Contains(':'))
            {
                HandleAdminCommand(currentCommand, numOfArguments);
                return;
            }

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

        private void HandleAdminCommand(string[] currentCommand, int numOfArguments)
        {
            int productId = 0;
            int amountOfCredits = 0;

            if(!adminCommands.ContainsKey(currentCommand[0]))
            {
                stregsystemCLI.DisplayAdminCommandNotFoundMessage();
                return;
            }

            try
            {
                switch (numOfArguments)
                {
                    case 1:
                        adminCommands[currentCommand[0]].DynamicInvoke();
                        break;
                    case 2:
                        Int32.TryParse(currentCommand[1], out productId);

                        if (productId < 1)
                        {
                            stregsystemCLI.DisplayNotValidProductID();
                        }
                        else
                        {
                            adminCommands[currentCommand[0]].DynamicInvoke(productId);
                        }
                        break;
                    case 3:
                        Int32.TryParse(currentCommand[2], out amountOfCredits);

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
            catch
            {
                stregsystemCLI.DisplayAdminCommandNotFoundMessage();
            }       
        }

        private void InsertCashAdminCommand(string userName, int amount)
        {
            User user;
            
            try
            {
                user = stregsystem.GetUser(userName);
            }
            catch (UserNotFoundException e)
            {
                stregsystemCLI.DisplayUserNotFound(e.userName);
                return;
            }

            stregsystem.AddCreditsToAccount(user, amount * 100);
            stregsystemCLI.DisplayInsertedCashToUser(user.userName, amount);
        }

        private string[] GetUserInput()
        {
            return Console.ReadLine().Split(' ');
        }

        private void UserInfoCommand(string[] currentCommand)
        {
            try
            {
                stregsystemCLI.DisplayUserInfo(currentCommand[0]);
            }
            catch (UserNotFoundException e)
            {
                stregsystemCLI.DisplayUserNotFound(e.userName);
            }
        }

        private void BuyProductCommand(string[] currentCommand)
        {
            User user;
            int productId = 0;
            Product product;
            BuyTransaction transaction;

            Int32.TryParse(currentCommand[1], out productId);
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
                stregsystemCLI.DisplayUserNotFound(e.userName);
                return;
            }

            catch (ProductNotFoundException e)
            {
                stregsystemCLI.DisplayProductNotFound(e.id);
                return;
            }

            catch (InsufficientCreditsException e)
            {
                stregsystemCLI.DisplayInsufficientCash(e.user.userName, e.product.name);
                return;
            }

            stregsystemCLI.DisplayUserBuysProduct(transaction);
        }

        private void BuyMultipleProductCommand(string[] currentCommand)
        {
            User user;
            int productId = 0;
            int amount = 0;
            Product product;
            BuyTransaction transaction;

            Int32.TryParse(currentCommand[1], out amount);
            if (amount < 1)
            {
                stregsystemCLI.DisplayNotValidAmountOfProduct();
                return;
            }

            Int32.TryParse(currentCommand[2], out productId);
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
                stregsystemCLI.DisplayUserNotFound(e.userName);
                return;
            }

            catch (ProductNotFoundException e)
            {
                stregsystemCLI.DisplayProductNotFound(e.id);
                return;
            }

            catch (InsufficientCreditsException e)
            {
                stregsystemCLI.DisplayInsufficientCash(e.user.userName, e.product.name);
                return;
            }

            stregsystemCLI.DisplayUserBuysProduct(transaction, amount);
        }
    }
}
