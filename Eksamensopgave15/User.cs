using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eksamensopgave15
{
    class User : IComparable
    {
        public int id { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string userName { get; set; }
        public string email { get; set; }
        public int balance { get; set; }

        public User(int id, string firstName, string lastName, string userName, string email)
        {
            this.id = id;
            this.firstName = firstName;
            this.lastName = lastName;
            this.userName = userName;
            this.email = email;
            balance = 0;
        }

        //Compares two users to see if they are the same object
        public int CompareTo(object obj)
        {
            if (obj == null) return 1;

            User other = obj as User;

            if (other != null)
            {
                return this.id.CompareTo(other.id);
            }
            else
                throw new ArgumentException("Object is not a user");
        }

        public override string ToString()
        {
            return "User's name: " + firstName + " " + lastName + "\nUser's email: " + email; 
        }

        //Equals to users by their id
        public override bool Equals(object obj)
        {
            User other = obj as User;

            if (other == null)
            {
                return false;
            }
            return id.Equals(other.id);
        }

        //Gets the hashcode by user id
        public override int GetHashCode()
        {
            return id.GetHashCode();
        }
    }
}
