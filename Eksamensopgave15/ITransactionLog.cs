using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eksamensopgave15
{
    //Interface for being able to switch how the transaction log is stored
    interface ITransactionLog
    {
        void WriteTransactionToLog(Transaction transaction);
        List<String[]> GetTransactionsFromLog(int numOfTransactions, string username);
    }
}
