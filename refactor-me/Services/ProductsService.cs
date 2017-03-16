using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using refactor_me.Api;
using refactor_me.Models;

namespace refactor_me.Services
{
    public class ProductsService : IProductsService
    {

        private readonly IProductsDatabase productsDatabase;
        private readonly IProductOptionsDatabase productOptionDatabase;

        public ProductsService(IProductsDatabase productsDatabase, IProductOptionsDatabase productOptionDatabase)
        {
            this.productsDatabase = productsDatabase;
            this.productOptionDatabase = productOptionDatabase;
        }

        public void Create(Product product)
        {
            this.productsDatabase.Save(product);
        }

        public void Delete(Guid id)
        {
            // Delete all referenced options before deleting the actual product.
            this.productOptionDatabase.DeleteOptionsGivenProductId(id);
            this.productsDatabase.Delete(id);
        }

        public List<Product> GetAll()
        {
            return this.productsDatabase.GetAll();
        }

        public Product GetProduct(Guid id)
        {
            return this.productsDatabase.Get(id);
        }

        public List<Product> SearchByName(string name)
        {
            return this.productsDatabase.SearchByName(name);
        }

        public void Update(Guid id, Product product)
        {
            product.Id = id;
            this.productsDatabase.Save(product);
        }
    }
}