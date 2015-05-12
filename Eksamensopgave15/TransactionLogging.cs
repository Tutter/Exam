using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Eksamensopgave15
{
    class TransactionLogging : ITransactionLog
    {
        private string path;

        public TransactionLogging()
        {
            path = @".\Transaction_Log.txt";
            //Creates a text log if it does not already exist
            if (!File.Exists(path))
            {
                FileStream fs = File.Create(path);
                fs.Close();
            }
        }

        //Writes a transaction to the file as a new line
        public void WriteTransactionToLog(Transaction transaction)
        {
            StreamWriter writer = File.AppendText(path);

            writer.WriteLine(transaction.ToString());

            writer.Close();
        }

        //Gets the next transaction id by reading from the log
        public int GetNextTransactionId()
        {
            int linesToSkip;
            string lastLine;
            string[] lineValues;

            //If the length of the file is not 0
            if (new FileInfo(@".\Transaction_Log.txt").Length != 0)
            {
                //Takes the last line of the file
                lastLine = File.ReadLines(path).Last();

                //If lastLine is an empty string then take the line before that
                if (lastLine == "")
                {
                    linesToSkip = File.ReadLines(path).Count() - 2;
                    lastLine = File.ReadLines(path).Skip(linesToSkip).First();
                }

                //Splits the line everytime a space occures and puts the values into an array of strings
                lineValues = lastLine.Split(' ');

                //Returns lineValues[2] + 1 since that place in the array always will be a transaction id due to the way they are written to the file
                return Convert.ToInt32(lineValues[2]) + 1;
            }

            return 1;
        }

        //Gets a given number of a users latest transactions from the log
        public List<String[]> GetTransactionsFromLog(int numOfTransactions, string username)
        {
            int linesInFile = File.ReadLines(path).Count();
            int transactionsFound = 0;
            string line;
            string[] splitLine;
            int counter = 1;
            List<String[]> transactions = new List<string[]>();

            //While the number of transactions found is less than the number of transactions to get and counter is equal to or less than the number of lines in the file
            while (transactionsFound < numOfTransactions && counter <= linesInFile)
            {
                //Takes a line from the bottom of the file and up
                line = File.ReadLines(path).Skip(linesInFile - counter).First();

                //If the line contains the given username
                if (line.Contains(" " + username + " "))
                {
                    splitLine = line.Split(';');
                    transactions.Add(splitLine);
                    transactionsFound++;
                }
                counter++;
            }

            return transactions;
        }
    }
}
