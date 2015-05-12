using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Eksamensopgave15
{
    class UserList : IUserDataHandling
    {
        public List<User> users;
        enum AddUserReturns { success, emailInvalid, usernameInvalid, allInvalid };
        int debug;
        bool invalid;

        public UserList()
        {
            users = new List<User>();
            DebugUsers();
        }

        //Adds a user to the userlist if certain criteria are met
        public int AddUser(string firstName, string lastName, string username, string email)
        {
            int id;
            bool emailIsValid;
            bool usernameIsValid;
            bool userameIsUnique;

            emailIsValid = IsValidEmail(email);
            usernameIsValid = IsValidUsername(username);
            userameIsUnique = IsUniqueUsername(username);

            //If the email is valisd and the username is valid and the username is unique then add the user to the userlist and return success(0)
            if (emailIsValid == true && usernameIsValid == true && userameIsUnique == true)
            {
                id = GetNextUserId();
                users.Add(new User(id, firstName, lastName, username, email));
                return (int)AddUserReturns.success;
            }

            //If the email is not valid and the username is not valid then return allInvalid(3)
            if (emailIsValid == false && (usernameIsValid == false || userameIsUnique == false))
                return (int)AddUserReturns.allInvalid;
            //Else if the email is not valid then return emailInvalid(1)
            else if (emailIsValid == false)
                return (int)AddUserReturns.emailInvalid;
            //else return userNameInvalid(2)
            else
                return (int)AddUserReturns.usernameInvalid;
        }

        //Gets the next user id by checking the last userlist members id
        private int GetNextUserId()
        {
            if (users.Count() == 0)
            {
                return 1;
            }

            return users[users.Count() - 1].id + 1;
        }

        //Found at https://msdn.microsoft.com/en-us/library/vstudio/01escwtf%28v=vs.100%29.aspx
        public bool IsValidEmail(string email)
        {
            invalid = false;
            if (String.IsNullOrEmpty(email))
                return false;

            // Use IdnMapping class to convert Unicode domain names.
            email = Regex.Replace(email, @"(@)(.+)$", this.DomainMapper);
            if (invalid)
                return false;

            // Return true if strIn is in valid e-mail format.
            return Regex.IsMatch(email,
                   @"^(?("")(""[^""]+?""@)|(([0-9a-z]((\.(?!\.))|[-\w])*)(?<=[0-9a-z])@))" +
                   @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9]{2,17}))$",
                   RegexOptions.IgnoreCase);
        }

        //Part of https://msdn.microsoft.com/en-us/library/vstudio/01escwtf%28v=vs.100%29.aspx
        private string DomainMapper(Match match)
        {
            // IdnMapping class with default property values.
            IdnMapping idn = new IdnMapping();

            string domainName = match.Groups[2].Value;
            try
            {
                domainName = idn.GetAscii(domainName);
            }
            catch (ArgumentException)
            {
                invalid = true;
            }
            return match.Groups[1].Value + domainName;
        }

        //Found at http://stackoverflow.com/questions/1046740/how-can-i-validate-a-string-to-only-allow-alphanumeric-characters-in-it
        private bool IsValidUsername(string userame)
        {
            Regex r = new Regex("^[a-zA-Z0-9]*$");
            if (r.IsMatch(userame))
            {
                return true;
            }

            return false;
        }

        //Checks if the given username is already in the userlist
        private bool IsUniqueUsername(string userName)
        {
            foreach (User user in users)
            {
                if (user.username == userName)
                    return false;
            }

            return true;
        }

        //Gets a user by username
        public User GetUserByUsername(string username)
        {
            foreach (User user in users)
            {
                if (user.username == username)
                    return user;
            }
            throw new UserNotFoundException(username);
        }

        //Fills the userlist with some users
        private void DebugUsers()
        {
            debug = AddUser("John", "Schmidt", "Smitty", "Schmidt@gmail.com");
            debug = AddUser("John", "Schmidt", "Smitty", "Schmidt2@gmail.com");
            debug = AddUser("Rick", "Rollin", "Ricky", "Never.Gonna_Give-You@up.com");
            debug = AddUser("Malcom", "Mals", "Mally", "M@ll£y@gmail.com");
            debug = AddUser("James", "Fidelnurr", "J@s", "James@gmail.com");
        }
    }
}
