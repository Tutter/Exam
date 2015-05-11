using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eksamensopgave15
{
    class InsufficientCreditsException : Exception
    {
        public User user;
        public Product product;

        public InsufficientCreditsException(User user, Product product)            
        {
            this.user = user;
            this.product = product;
        }
    }
}
