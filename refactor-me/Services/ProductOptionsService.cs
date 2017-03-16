using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using refactor_me.Api;
using refactor_me.Models;

namespace refactor_me.Services
{
    public class ProductOptionsService : IProductOptionsService
    {

        private readonly IProductOptionsDatabase productOptionDatabase;

        public ProductOptionsService(IProductOptionsDatabase productOptionDatabase)
        {
            this.productOptionDatabase = productOptionDatabase;
        }

        public void CreateOption(Guid id, ProductOption option)
        {
            option.ProductId = id;
            this.productOptionDatabase.Save(option);
        }

        public void DeleteOption(Guid id)
        {
            this.productOptionDatabase.Delete(id);
        }

        public ProductOption GetOption(Guid productId, Guid id)
        {
            return this.productOptionDatabase.Get(id);
        }

        public List<ProductOption> GetOptions(Guid productId)
        {
            return this.productOptionDatabase.GetProductOptions(productId);
        }

        public void UpdateOption(Guid id, ProductOption option)
        {
            option.Id = id;
            this.productOptionDatabase.Save(option);
        }
    }
}