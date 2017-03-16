using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using refactor_me.Api;
using refactor_me.Models;

namespace UnitTestRefactorMe
{
    public sealed class MockDataUtil
    {

        public static readonly string MOCK_PRODUCT_GUID_1 = "eb70e2d3-2287-47f0-a769-eb12a85bfd73";
        public static readonly string MOCK_PRODUCT_GUID_2 = "94ef8ccc-bcf4-4e8d-99f9-233eb8d2946c";
        public static readonly string MOCK_PRODUCT_OPTION_GUID_1 = "9a8c9c08-4007-42a1-90b4-a3869740ff4d";
        public static readonly string MOCK_PRODUCT_OPTION_GUID_2 = "6b9cbad5-3929-4865-a672-818fdaf2265a";
        public static readonly Product MOCK_PRODUCT_1 = new Product { Id = new Guid(MOCK_PRODUCT_GUID_1), Name = "mock1", Description = "mockDescription1", Price = 1, DeliveryPrice = 1 };
        public static readonly Product MOCK_PRODUCT_2 = new Product { Id = new Guid(MOCK_PRODUCT_GUID_2), Name = "mock2", Description = "mockDescription2", Price = 2, DeliveryPrice = 2 };
        public static readonly ProductOption MOCK_OPTION_1 = new ProductOption { ProductId = new Guid(MOCK_PRODUCT_GUID_1), Id = new Guid(MOCK_PRODUCT_OPTION_GUID_1), Description = "productOptionDescription1", Name = "productOption1" };
        public static readonly ProductOption MOCK_OPTION_2 = new ProductOption { ProductId = new Guid(MOCK_PRODUCT_GUID_1), Id = new Guid(MOCK_PRODUCT_OPTION_GUID_2), Description = "productOptionDescription2", Name = "productOption2" };


        private MockDataUtil()
        {

        }

        public static IProductsDatabase getMockProductDatabase()
        {
            return new MockProductDatabase(new List<Product> { MOCK_PRODUCT_1, MOCK_PRODUCT_2 });
        }

        public static IProductOptionsDatabase getMockProductOptionDatabase()
        {
            return new MockProductOptionsDatabase(new List<ProductOption> { MOCK_OPTION_1, MOCK_OPTION_2 });
        }

    }
}
