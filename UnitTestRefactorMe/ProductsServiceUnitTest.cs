using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using refactor_me.Api;
using refactor_me.Models;
using refactor_me.Services;

namespace UnitTestRefactorMe
{
    [TestClass]
    public class ProductsServiceUnitTest
    {

        private IProductsDatabase mockProductDatabase;
        private IProductOptionsDatabase mockProductOptionsDatabase;
        private IProductsService service;

        [TestInitialize]
        public void Setup()
        {
            this.mockProductDatabase = MockDataUtil.getMockProductDatabase();
            this.mockProductOptionsDatabase = MockDataUtil.getMockProductOptionDatabase(); 
            this.service = new ProductsService(this.mockProductDatabase, this.mockProductOptionsDatabase);
        }

        [TestMethod]
        public void TestGetAllReturnsAllProducts()
        {
            var products = this.service.GetAll().Items;
            Assert.AreEqual(2, products.Count);
        }

        [TestMethod]
        public void TestCreate()
        {
            var newId = Guid.NewGuid();
            var newProduct = new Product { Id = newId, Name = "mock3", Description = "mockDescription3", Price = 3, DeliveryPrice = 3};

            this.service.Create(newProduct);

            var product = this.service.GetProduct(newId);
            Assert.IsTrue(newProduct.Equals(product));
        }

        [TestMethod]
        public void TestGetProductReturnsCorrectProduct()
        {
            var product = this.service.GetProduct(new Guid(MockDataUtil.MOCK_PRODUCT_GUID_1));

            Assert.IsTrue(product.Equals(MockDataUtil.MOCK_PRODUCT_1));
        }

        [TestMethod]
        public void TestSearchByName()
        {
            var products = this.service.SearchByName(MockDataUtil.MOCK_PRODUCT_1.Name).Items;

            Assert.AreEqual(1, products.Count);
            Assert.IsTrue(products[0].Equals(MockDataUtil.MOCK_PRODUCT_1));

        }

        [TestMethod]
        public void TestUpdate()
        {
            var updatedName = "updateMock1";
            var updatedDescription = "updatedDescription1";
            var updatedPrice = 23;
            var updatedDeliveryPrice = 25;

            var updatedMock1 = new Product { Name = updatedName, Description = updatedDescription, Price = updatedPrice, DeliveryPrice = updatedDeliveryPrice };
            var mock1Guid = new Guid(MockDataUtil.MOCK_PRODUCT_GUID_1);
            this.service.Update(mock1Guid, updatedMock1);

            var product = this.service.GetProduct(mock1Guid);
            Assert.AreEqual(updatedName, product.Name);
            Assert.AreEqual(updatedDescription, product.Description);
            Assert.AreEqual(updatedPrice, product.Price);
            Assert.AreEqual(updatedDeliveryPrice, product.DeliveryPrice);
        }

        [TestMethod]
        public void TestDeleteRemovesProduct()
        {
            this.service.Delete(new Guid(MockDataUtil.MOCK_PRODUCT_GUID_1));

            var products = this.service.GetAll().Items;
            Assert.AreEqual(1, products.Count);
            Assert.AreEqual(new Guid(MockDataUtil.MOCK_PRODUCT_GUID_2), products[0].Id);
        }

        [TestMethod]
        public void TestDeletingProductRemovesProductOptions()
        {
            var id = new Guid(MockDataUtil.MOCK_PRODUCT_GUID_1);
            var options = this.mockProductOptionsDatabase.GetProductOptions(id).Items;
            Assert.AreEqual(2, options.Count);

            this.service.Delete(id);

            options = this.mockProductOptionsDatabase.GetProductOptions(id).Items;
            Assert.AreEqual(0, options.Count);
        }
    
    }
}
