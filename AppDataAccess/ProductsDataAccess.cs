using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



using AppDataAccess.Models;


namespace AppDataAccess
{
    public class ProductsDataAccess
    {

        private string path = @"./DBproduct.csv";

        private void readProducts()
        {
            using (var reader = new StreamReader(path))
            {
                product.Clear();
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    string[] values = line.Split(';');

                    Products byr = new Products()
                    {
                        id = Convert.ToInt32(values[0].Trim()),
                        name = values[1].Trim(),
                        description = values[2],
                        inventory = Convert.ToInt32(values[3].Trim()),
                        price = Convert.ToDecimal(values[4].Trim()),
                    };

                    product.Add(byr);
                }
            }
        }

        private void saveProducts()
        {
            using (var writer = new StreamWriter(path))
            {
                foreach (Products prd in product)
                {
                    string id = prd.id.ToString();
                    string name = prd.name;
                    string description = prd.description;
                    string inventory = prd.inventory.ToString();
                    string price = prd.price.ToString();

                    string line = string.Format("{0};{1};{2};{3};{4}",
                        id, name, description, inventory, price);

                    writer.WriteLine(line);
                }
                writer.Close();
            }
        }


        public ObservableCollection<Products> product { get; set; } = new ObservableCollection<Products>();
        public ProductsDataAccess()
        {
            readProducts();
        }



        public void addProduct(Products newProduct)
        {
            product.Add(newProduct);
            saveProducts();
        }

        public void removeProduct(int id)
        {
            Products temp = product.First(x => x.id == id);
            product.Remove(temp);
            saveProducts();
        }

        public void editeProduct(Products updateProduct)
        {
            Products temp = product.First(x => x.id == updateProduct.id);
            int index = product.IndexOf(temp);
            product[index] = updateProduct;
            saveProducts();
        }

        public int getNextId()
        {
            int index = product.Any() ? product.Max(x => x.id) + 1 : 1;
            return index;
        }
    }
}
