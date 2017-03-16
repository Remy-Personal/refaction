using System;
using System.Collections.Generic;
using refactor_me.Models;

namespace refactor_me.Api
{
    public interface IProductsService
    {

        List<Product> GetAll();

        List<Product> SearchByName(string name);

        Product GetProduct(Guid id);

        void Create(Product product);

        void Update(Guid id, Product product);

        void Delete(Guid id);

    }
}
