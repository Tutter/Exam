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

        //Overloaded method for buying one product
        public BuyTransaction BuyProduct(User user, Product product, int price)
        {
            BuyTransaction transaction = new BuyTransaction(transactionLogging.GetNextTransactionId(), user, product, product.price);

            ExecuteTransaction(transaction);

            transactionLogging.WriteTransactionToLog(transaction);

            return transaction;
        }

        //Overloaded method for buying multiple products
        public BuyTransaction BuyProduct(User user, Product product, int price, int amount)
        {
            BuyTransaction transaction = new BuyTransaction(transactionLogging.GetNextTransactionId(), user, product, product.price, amount);

            ExecuteTransaction(transaction);

            transactionLogging.WriteTransactionToLog(transaction);

            return transaction;
        }

        //Inserts credits to a users account
        public InsertCashTransaction AddCreditsToAccount(User user, int amount)
        {
            InsertCashTransaction transaction = new InsertCashTransaction(transactionLogging.GetNextTransactionId(), user, amount);
            
            ExecuteTransaction(transaction);

            transactionLogging.WriteTransactionToLog(transaction);

            return transaction;
        }

        //Exectues a transaction
        private void ExecuteTransaction(Transaction transaction)
        {
            transaction.Execute();
        }

        //Gets a product by id
        public Product GetProduct(int id)
        {
            return productList.products[id];
        }

        //Gets a user by username
        public User GetUser(string userName)
        {
            return userList.GetUserByUsername(userName);
        }

        //Gets a list of a specified number of transactions
        public List<string[]> GetTransactionList(int numOfTransactions, string userName)
        {
            return transactionLogging.GetTransactionsFromLog(numOfTransactions, userName);
        }

        //Gets active products
        public List<Product> GetActiveProducts()
        {
            List<Product> activeProducts = new List<Product>();
            activeProducts = productList.products.Where(product => product.active).ToList<Product>();

            return activeProducts;
        }

        //Activates a product by id
        public void ActivateProduct(int id)
        {
            productList.ActivateProductByID(id);
        }

        //Deactivates a product by id
        public void DeactivateProduct(int id)
        {
            productList.DeactivateProductByID(id);
        }

        //Activates that a product by id can be bought on credits
        public void ActivateBuyOnCredit(int id)
        {
            productList.ActivateBuyOnCreditsByID(id);
        }

        //Deactivates that a product by id can be bought on credits
        public void DeactivateBuyOnCredit(int id)
        {
            productList.DeactivateBuyOnCreditsByID(id);
        }
    }
}
