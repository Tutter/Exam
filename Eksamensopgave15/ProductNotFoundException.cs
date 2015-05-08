using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eksamensopgave15
{
    class ProductNotFoundException : Exception
    {
        public int id;

        public ProductNotFoundException(int id)
        {
            this.id = id;
        }
    }
}
