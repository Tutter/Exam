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

        //Reads the products from a products file
        private void ReadProducts()
        {
            int id;
            string name;
            int price;
            bool activeProduct;

            StreamReader reader = new StreamReader(@".\products.csv");

            reader.ReadLine();

            //While the streamreader is not at the end of the file
            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine();

                //Splits the sting every time a ';' is encountered and puts it into an array of strings
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

        //Gets a product by id
        public Product GetProductByID(int id)
        {
            foreach (Product product in products)
            {
                if (product.id == id)
                    return product;
            }

            throw new ProductNotFoundException(id);
        }

        //Activates a product by id
        public void ActivateProductByID(int id)
        {
            foreach (Product product in products)
            {
                if (product.id == id)
                    product.active = true;
            }
        }

        //Deactivates a product by id
        public void DeactivateProductByID(int id)
        {
            foreach (Product product in products)
            {
                if (product.id == id)
                    product.active = false;
            }
        }

        //Activates that a product by id can be bought on credit
        public void ActivateBuyOnCreditsByID(int id)
        {
            foreach (Product product in products)
            {
                if (product.id == id)
                    product.canBeBoughtOnCredit = true;
            }
        }

        //Deactivates that a product by id can be bought on credits
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
