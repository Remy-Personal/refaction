using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using refactor_me.Api;
using refactor_me.Models;

namespace UnitTestRefactorMe
{
    class MockProductOptionsDatabase : IProductOptionsDatabase
    {

        private List<ProductOption> options = new List<ProductOption>();

        public MockProductOptionsDatabase(List<ProductOption> options)
        {
            this.options.AddRange(options);
        }

        public void Delete(Guid id)
        {
            var removedElements = this.options.RemoveAll(option => option.Id.Equals(id));
            if (removedElements < 1)
            {
                throw new ArgumentException(String.Format("No product option found with Id: {0}.", id));
            }
        }

        public void DeleteOptionsGivenProductId(Guid productId)
        {
            var removedElements = this.options.RemoveAll(option => option.ProductId.Equals(productId));
            if (removedElements < 1)
            {
                throw new ArgumentException(String.Format("No product options found for product Id: {0}.", productId));
            }
        }

        public ProductOption Get(Guid id)
        {
            return this.options.Single(option => option.Id == id);
        }

        public List<ProductOption> GetProductOptions(Guid productId)
        {
            return this.options.Where(option => option.ProductId.Equals(productId)).ToList();
        }

        public void Save(ProductOption productOption)
        {
            // Remove old option entry if its already in the list.
            this.options = this.options.Where(option => !option.Id.Equals(productOption.Id)).ToList();
            this.options.Add(productOption);
        }
    }
}
