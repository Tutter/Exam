using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Eksamensopgave15
{
    class ProductList
    {
        public List<Product> products;

        public ProductList()
        {
            products = new List<Product>();
            ReadProducts();
        }

        private void ReadProducts()
        {
            int id;
            string name;
            int price;
            bool activeProduct;

            StreamReader reader = new StreamReader(@".\products.csv");

            reader.ReadLine();

            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine();
                string[] lineValues = RemoveHTMLTags(line).Split(';');

                id = (Convert.ToInt32(lineValues[0]));
                name = (lineValues[1]);
                price = (Convert.ToInt32(lineValues[2]));

                if (lineValues[3] == "0")
                    activeProduct = false;
                else
                    activeProduct = true;

                products.Add(new Product(id, name, price, activeProduct));            
            }
        }

        //Found at http://www.dotnetperls.com/remove-html-tags
        private string RemoveHTMLTags(string line)
        {
                return Regex.Replace(line, "<.*?>", string.Empty);
        }

        public Product GetProductByID(int id)
        {
            foreach (Product product in products)
            {
                if (product.id == id)
                    return product;
            }

            throw new ProductNotFoundException(id);
        }

        public void ActivateProductByID(int id)
        {
            foreach (Product product in products)
            {
                if (product.id == id)
                    product.active = true;
            }
        }

        public void DeactivateProductByID(int id)
        {
            foreach (Product product in products)
            {
                if (product.id == id)
                    product.active = false;
            }
        }

        public void ActivateBuyOnCreditsByID(int id)
        {
            foreach (Product product in products)
            {
                if (product.id == id)
                    product.canBeBoughtOnCredit = true;
            }
        }

        public void DeactivateBuyOnCreditsByID(int id)
        {
            foreach (Product product in products)
            {
                if (product.id == id)
                    product.canBeBoughtOnCredit = false;
            }
        }
    }
}
