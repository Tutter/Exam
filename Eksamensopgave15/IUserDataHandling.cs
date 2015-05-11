using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eksamensopgave15
{
    //Interface for being able to change the userlist for another way of storage
    interface IUserDataHandling
    {
        int AddUser(string firstName, string lastName, string userName, string email);
        
        string ReadUser(string userName);
    }
}
