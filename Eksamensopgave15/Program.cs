using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eksamensopgave15
{
    class Program
    {
        static void Main(string[] args)
        {
            Stregsystem stregsystem = new Stregsystem();
            StregsystemCLI cli = new StregsystemCLI(stregsystem);
            StregsystemCommandParser commandParser = new StregsystemCommandParser(stregsystem, cli);
            cli.Start(commandParser);
        }
    }
}
