using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eksamensopgave15
{
    interface IStregsystemUI
    {
        void DisplayUserNotFound(string userName);
        void DisplayProductNotFound(int productId);
        void DisplayUserInfo(string userName);
        void DisplayTooManyArgumentsError();
        void DisplayAdminCommandNotFoundMessage();
        void DisplayUserBuysProduct(BuyTransaction transaction);
        void DisplayUserBuysProduct(BuyTransaction transaction, int amount);
        void Close();
        void DisplayInsufficientCash();
        void DisplayGeneralError(string errorString);
    }
}
