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

        public BuyTransaction BuyProduct(User user, Product product, int price)
        {
            BuyTransaction transaction = new BuyTransaction(transactionLogging.GetNextTransactionId(), user, product, product.price);

            ExecuteTransaction(transaction);

            return transaction;
        }

        public BuyTransaction BuyProduct(User user, Product product, int price, int amount)
        {
            BuyTransaction transaction = new BuyTransaction(transactionLogging.GetNextTransactionId(), user, product, product.price, amount);

            ExecuteTransaction(transaction);

            transactionLogging.WriteTransactionToFile(transaction);

            return transaction;
        }

        public InsertCashTransaction AddCreditsToAccount(User user, int amount)
        {
            InsertCashTransaction transaction = new InsertCashTransaction(transactionLogging.GetNextTransactionId(), user, amount);
            
            ExecuteTransaction(transaction);

            transactionLogging.WriteTransactionToFile(transaction);

            return transaction;
        }

        private void ExecuteTransaction(Transaction transaction)
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

        public void ActivateProduct(int id)
        {
            productList.ActivateProductByID(id);
        }

        public void DeactivateProduct(int id)
        {
            productList.DeactivateProductByID(id);
        }

        public void ActivateBuyOnCredit(int id)
        {
            productList.ActivateBuyOnCreditsByID(id);
        }

        public void DeactivateBuyOnCredit(int id)
        {
            productList.DeactivateBuyOnCreditsByID(id);
        }
    }
}
