using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eksamensopgave15
{
    class Stregsystem
    {
        public UserList userList;
        public ProductList productList;
        public TransactionLogging transactionLogging;

        public Stregsystem()
        {
            userList = new UserList();
            productList = new ProductList();
            transactionLogging = new TransactionLogging();
        }

        public void BuyProduct(User user, Product product)
        {
            Transaction transaction = new BuyTransaction(user, product, product.price);
        }

        public void AddCreditsToAccount(User user, int amount)
        {
            Transaction transaction = new InsertCashTransaction(user, amount);
        }

        public void ExecuteTransaction(Transaction transaction)
        {
            transaction.Execute();
        }

        public Product GetProduct(int id)
        {
            return productList.products[id];
        }

        public User GetUser(string userName)
        {
            return userList.GetUserByUserName(userName);
        }

        public List<string> GetTransactionList(int numOfTransactions, string userName)
        {
            return transactionLogging.GetTransactionsFromLog(numOfTransactions, userName);
        }

        public List<Product> GetActiveProducts()
        {
            List<Product> activeProducts = new List<Product>();
            activeProducts = productList.products.Where(product => product.active).ToList<Product>();

            return activeProducts;
        }
    }
}
