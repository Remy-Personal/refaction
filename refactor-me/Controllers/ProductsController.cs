using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using refactor_me.Api;
using refactor_me.Models;

namespace refactor_me.Controllers
{
    [RoutePrefix("products")]
    public class ProductsController : ApiController
    {

        private readonly IProductsService productsService;
        private readonly IProductOptionsService productOptionService;

        public ProductsController(IProductsService productsService, IProductOptionsService productOptionsService)
        {
            this.productsService = productsService;
            this.productOptionService = productOptionsService;
        }

        [Route]
        [HttpGet]
        public List<Product> GetAll()
        {
            return this.productsService.GetAll();
        }

        [Route]
        [HttpGet]
        public List<Product> SearchByName(string name)
        {
            return this.productsService.SearchByName(name);
        }

        [Route("{id}")]
        [HttpGet]
        public Product GetProduct(Guid id)
        {
            return this.productsService.GetProduct(id);
        }

        [Route]
        [HttpPost]
        public void Create(Product product)
        {
            this.productsService.Create(product);
        }

        [Route("{id}")]
        [HttpPut]
        public void Update(Guid id, Product product)
        {
            this.productsService.Update(id, product);
        }

        [Route("{id}")]
        [HttpDelete]
        public void Delete(Guid id)
        {
            this.productsService.Delete(id);
        }

        [Route("{productId}/options")]
        [HttpGet]
        public List<ProductOption> GetOptions(Guid productId)
        {
            return this.productOptionService.GetOptions(productId);
        }

        [Route("{productId}/options/{id}")]
        [HttpGet]
        public ProductOption GetOption(Guid productId, Guid id)
        {
            return this.productOptionService.GetOption(productId, id);
        }

        [Route("{productId}/options")]
        [HttpPost]
        public void CreateOption(Guid productId, ProductOption option)
        {
            this.productOptionService.CreateOption(productId, option);
        }

        [Route("{productId}/options/{id}")]
        [HttpPut]
        public void UpdateOption(Guid id, ProductOption option)
        {
            this.productOptionService.UpdateOption(id, option);
        }

        [Route("{productId}/options/{id}")]
        [HttpDelete]
        public void DeleteOption(Guid id)
        {
            this.productOptionService.DeleteOption(id);
        }
    }
}
