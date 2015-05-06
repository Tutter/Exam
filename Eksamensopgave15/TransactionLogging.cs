﻿using System;
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

        public int GetNextTransactionId()
        {
            string lastLine;
            string[] lineValues;

            lastLine = File.ReadLines(path).Last();
            lineValues = lastLine.Split(' ');

            return Convert.ToInt32(lineValues[2]);
        }

        public List<String> GetTransactionsFromLog(int numOfTransactions, string userName)
        {
            int linesInFile = FindLengthOfFile();
        }

        private int FindLengthOfFile()
        {
            int count = 0;
            StreamReader reader = new StreamReader(File.OpenRead(path));

            while (!reader.EndOfStream)
                count++;

            return count;
        }
    }
}