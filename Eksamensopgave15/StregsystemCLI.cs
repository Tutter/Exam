using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eksamensopgave15
{
    class StregsystemCLI : IStregsystemUI
    {
        Stregsystem stregsystem;

        private void Start()
        {

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
 	        throw new NotImplementedException();
        }

        public void DisplayTooManyArgumentsError()
        {
 	        throw new NotImplementedException();
        }

        public void DisplayAdminCommandNotFoundMessage()
        {
 	        throw new NotImplementedException();
        }

        public void DisplayUserBuysProduct(BuyTransaction transaction)
        {
 	        throw new NotImplementedException();
        }

        public void DisplayUserBuysProduct(int count)
        {
 	        throw new NotImplementedException();
        }

        public void Close()
        {
 	        throw new NotImplementedException();
        }

        public void DisplayInsufficientCash()
        {
 	        throw new NotImplementedException();
        }

        public void DisplayGeneralError(string errorString)
        {
 	        throw new NotImplementedException();
        }
    }
}
