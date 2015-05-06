using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Eksamensopgave15
{
    class UserList
    {
        private List<User> userList;
        private int nextId;
        enum AddUserReturns { success, emailInvalid, userNameInvalid, allInvalid};

        public UserList()
        {
            userList = new List<User>();
        }

        public int addUser(string firstName, string lastName, string userName, string email)
        {
            int id;
            bool emailIsValid;
            bool userNameIsValid;

            id = GetNextUserId();
            emailIsValid = IsValidEmail(email);
            userNameIsValid = IsValidUsername(userName);

            if (emailIsValid == true && userNameIsValid == true)
            {
                userList.Add(new User(id, firstName, lastName, userName, email));
                return (int)AddUserReturns.success;
            }

            if (emailIsValid == false && userNameIsValid == false)
                return (int)AddUserReturns.allInvalid;

            if (emailIsValid == false)
                return (int)AddUserReturns.emailInvalid;
            else
                return (int)AddUserReturns.userNameInvalid;    
        }

        private int GetNextUserId()
        {
            if (userList.Count() == 0)
            {
                return 1;
            }

            return userList[userList.Count() - 1].id + 1;
        }

        //Found at http://stackoverflow.com/questions/1365407/c-sharp-code-to-validate-email-address
        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        //Found at http://stackoverflow.com/questions/1046740/how-can-i-validate-a-string-to-only-allow-alphanumeric-characters-in-it
        private bool IsValidUsername(string userName)
        {
            Regex r = new Regex("^[a-zA-Z0-9]*$");
            if (r.IsMatch(userName)) 
            {
                return true;
            }

            return false;
        }
    }
}
