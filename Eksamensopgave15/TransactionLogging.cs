using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Eksamensopgave15
{
    class TransactionLogging
    {
        private string path;

        public TransactionLogging()
        {
            string path = @"\Transaction_Log.txt";
            if (!File.Exists(path))
            {
                CreateLog();
            }
        }

        private void CreateLog()
        {
            File.Create(path);
        }

        public void WriteToFile(Transaction transaction)
        {
            StreamWriter writer = File.AppendText(path);

            writer.WriteLine(transaction.ToString());
        }

        public int GetNextId()
        {
            string lastLine;
            string[] lineValues;

            lastLine = File.ReadLines(path).Last();
            lineValues = lastLine.Split(' ');

            return Convert.ToInt32(lineValues[2]);
        }
    }
}
