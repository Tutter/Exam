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
        enum AddUserReturns { success, emailInvalid, userNameInvalid, allInvalid };
        int debug;
        bool invalid;

        public UserList()
        {
            users = new List<User>();
            DebugUsers();
        }

        public int AddUser(string firstName, string lastName, string userName, string email)
        {
            int id;
            bool emailIsValid;
            bool userNameIsValid;
            bool userNameIsUnique;

            id = GetNextUserId();
            emailIsValid = IsValidEmail(email);
            userNameIsValid = IsValidUsername(userName);
            userNameIsUnique = IsUniqueUsername(userName);

            if (emailIsValid == true && userNameIsValid == true && userNameIsUnique == true)
            {
                users.Add(new User(id, firstName, lastName, userName, email));
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
        private bool IsValidUsername(string userName)
        {
            Regex r = new Regex("^[a-zA-Z0-9]*$");
            if (r.IsMatch(userName))
            {
                return true;
            }

            return false;
        }

        private bool IsUniqueUsername(string userName)
        {
            foreach (User user in users)
            {
                if (user.userName == userName)
                    return false;
            }

            return true;
        }

        public User GetUserByUserName(string userName)
        {
            foreach (User user in users)
            {
                if (user.userName == userName)
                    return user;
            }
            throw new UserNotFoundException(userName);
        }

        public string ReadUser(string userName)
        {
            foreach (User user in users)
            {
                if (user.userName == userName)
                    return "ID: " + user.id + "\nName: " + user.firstName + " " + user.lastName + "\nUsername: " + user.userName + "\nEmail: " + user.email;
            }
            throw new UserNotFoundException(userName);
        }

        private void DebugUsers()
        {
            debug = AddUser("Tobias", "Bøgedal", "Tutter", "tuttanium@gmail.com");
            debug = AddUser("August", "Kørvell", "Aggi", "Ag_gi.so-ft@gmail.com");
            debug = AddUser("Daniel", "Bøgedal", "Muggi", "tuttanium@gmail.com");
            debug = AddUser("Tobias", "Bøgedal", "Tut@@ter", "tuttaasdnium@gmail.com");
        }
    }
}
