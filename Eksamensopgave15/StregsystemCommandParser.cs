using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eksamensopgave15
{
    class StregsystemCommandParser
    {   
        private string[] currentCommand;
        public Stregsystem stregsystem;
        public StregsystemCLI stregsystemCLI;

        public StregsystemCommandParser(Stregsystem stregsystem, StregsystemCLI stregsystemCLI)
        {
            this.stregsystem = stregsystem;
            this.stregsystemCLI = stregsystemCLI;
        }

        public void ParseCommand()
        {
            currentCommand = Console.ReadLine().Split(' ');

            if (currentCommand.Count() == 1)
            {

            }
            else if (currentCommand.Count() == 2)
            {

            }
            else if (currentCommand.Count() == 3)
            {

            }
            else
                stregsystemCLI.DisplayTooManyArgumentsError();
        }
    }
}
