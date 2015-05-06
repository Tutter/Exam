using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eksamensopgave15
{
    class InsufficientCreditsException : Exception
    {
        public InsufficientCreditsException(User user, Product product) 
            : base("User: " + user.userName + " has insufficient credits for " + product.name)
        {

        }
    }
}
