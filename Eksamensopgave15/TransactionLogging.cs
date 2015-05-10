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
            path = @".\Transaction_Log.txt";
            if (!File.Exists(path))
            {
                FileStream fs = File.Create(path);
                fs.Close();
            }
        }

        public void WriteTransactionToFile(Transaction transaction)
        {
            StreamWriter writer = File.AppendText(path);

            writer.WriteLine(transaction.ToString());

            writer.Close();
        }

        public int GetNextTransactionId()
        {
            int linesToSkip;
            string lastLine;
            string[] lineValues;

            if (new FileInfo(@".\Transaction_Log.txt").Length != 0)
            {
                lastLine = File.ReadLines(path).Last();

                if (lastLine == "")
                {
                    linesToSkip = File.ReadLines(path).Count() - 2;
                    lastLine = File.ReadLines(path).Skip(linesToSkip).First();
                }
                lineValues = lastLine.Split(' ');

                return Convert.ToInt32(lineValues[2]) + 1;
            }

            return 1;
        }

        public List<String> GetTransactionsFromLog(int numOfTransactions, string userName)
        {
            int linesInFile = File.ReadLines(path).Count();
            int transactionsFound = 0;
            string line;
            int counter = 1;
            List<String> transactions = new List<string>();


            while (transactionsFound < numOfTransactions && counter <= linesInFile)
            {
                line = File.ReadLines(path).Skip(linesInFile - counter).First();

                if (line.Contains(" " + userName + " "))
                {
                    transactions.Add(line);
                    transactionsFound++;
                }
                counter++;
            }

            return transactions;
        }
    }
}
