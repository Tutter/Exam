using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
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

            string path = Environment.CurrentDirectory;
            StreamReader reader = new StreamReader(File.OpenRead(path));
            List<Product> products = new List<Product>();

            reader.ReadLine();

            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine();
                string[] lineValues = line.Split(';');

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
    }
}
