using System;
using System.Collections.Generic;
using System.Linq;
using refactor_me.Api;
using refactor_me.Models;

namespace UnitTestRefactorMe
{
    public class MockProductDatabase : IProductsDatabase
    {
        private List<Product> products = new List<Product>();

        public MockProductDatabase(List<Product> products)
        {
            this.products.AddRange(products);
        }

        public void Delete(Guid id)
        {
            var removedElements = this.products.RemoveAll(product => product.Id.Equals(id));
            if (removedElements < 1)
            {
                throw new ArgumentException(String.Format("No product found with Id: {0}.", id));
            }

        }

        public Product Get(Guid id)
        {
            return this.products.Single(product => product.Id.Equals(id));
        }

        public List<Product> GetAll()
        {
            return this.products;
        }

        public void Save(Product newProduct)
        {
            // Remove old product entry if its already in the list.
            this.products = this.products.Where(product => !product.Id.Equals(newProduct.Id)).ToList();
            this.products.Add(newProduct);
        }

        public List<Product> SearchByName(string name)
        {
            return this.products.Where(product => product.Name.Equals(name)).ToList();
        }
    }
}
