using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using refactor_me.Models;

namespace refactor_me.Api
{
    public interface IProductsService
    {

        Products GetAll();

        Products SearchByName(string name);

        Product GetProduct(Guid id);

        void Create(Product product);

        void Update(Guid id, Product product);

        void Delete(Guid id);

    }
}
