using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using refactor_me.Api;
using refactor_me.Models;
using refactor_me.Services;

namespace UnitTestRefactorMe
{
    [TestClass]
    public class ProductOptionsServiceUnitTest
    {
        private IProductOptionsDatabase mockProductOptionsDatabase;
        private IProductOptionsService service;

        [TestInitialize]
        public void Setup()
        {
            this.mockProductOptionsDatabase = MockDataUtil.getMockProductOptionDatabase();
            this.service = new ProductOptionsService(this.mockProductOptionsDatabase);
        }

        [TestMethod]
        public void TestCreate()
        {
            var productId = Guid.NewGuid();
            var optionId = Guid.NewGuid();
            var optionName = "mockOptionName";
            var optionDescription = "mockOptionDescription";

            this.service.CreateOption(productId, new ProductOption { Id = optionId, Name = optionName, Description = optionDescription });

            var option = this.service.GetOption(productId, optionId);
            Assert.AreEqual(optionId, option.Id);
            Assert.AreEqual(productId, option.ProductId);
            Assert.AreEqual(optionName, option.Name);
            Assert.AreEqual(optionDescription, option.Description);
        }

        [TestMethod]
        public void TestDeleteOption()
        {
            var productId = new Guid(MockDataUtil.MOCK_PRODUCT_GUID_1);
            var option = this.service.GetOptions(productId).Items;
            Assert.AreEqual(2, option.Count);

            this.service.DeleteOption(new Guid(MockDataUtil.MOCK_PRODUCT_OPTION_GUID_1));

            option = this.service.GetOptions(productId).Items;
            Assert.AreEqual(1, option.Count);
        }

        [TestMethod]
        public void TestGetOption()
        {
            var option = this.service.GetOption(new Guid(MockDataUtil.MOCK_PRODUCT_GUID_1), new Guid(MockDataUtil.MOCK_PRODUCT_OPTION_GUID_1));

            Assert.IsTrue(option.Equals(MockDataUtil.MOCK_OPTION_1));

        }

        [TestMethod]
        public void TestGetAllOptionsForProduct()
        {
            var options = this.service.GetOptions(new Guid(MockDataUtil.MOCK_PRODUCT_GUID_1)).Items;

            Assert.AreEqual(2, options.Count);
        }

        [TestMethod]
        public void TestUpdateOptions()
        {
            var productId = new Guid(MockDataUtil.MOCK_PRODUCT_GUID_1);
            var newGuid = Guid.NewGuid();
            var updatedName = "updatedOptionName";
            var updatedDescription = "updatedoptionDescription";
            var updatedOption = new ProductOption { ProductId = productId, Name = updatedName, Description = updatedDescription};
            this.service.UpdateOption(newGuid, updatedOption);

            var option = this.service.GetOption(productId, newGuid);
            Assert.AreEqual(newGuid, option.Id);
            Assert.AreEqual(productId, option.ProductId);
            Assert.AreEqual(updatedName, option.Name);
            Assert.AreEqual(updatedDescription, option.Description);
        }

    }
}
