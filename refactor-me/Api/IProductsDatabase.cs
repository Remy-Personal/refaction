using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using refactor_me.Models;

namespace refactor_me.Api
{
    public interface IProductsDatabase
    {

        List<Product> GetAll();

        Product Get(Guid id);

        void Save(Product product);

        void Delete(Guid id);

        List<Product> SearchByName(string name);
    }
}
