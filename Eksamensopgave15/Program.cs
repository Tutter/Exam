using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eksamensopgave15
{
    class Program
    {
        static void Main(string[] args)
        {
            StregsystemCLI stregsystemCLI = new StregsystemCLI();

            stregsystemCLI.Start();

            Console.ReadKey();
        }
    }
}
