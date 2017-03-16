using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using refactor_me.Api;
using refactor_me.Models;

namespace refactor_me.Services
{
    public class ProductOptionsDatabase : DatabaseOperations<ProductOption>, IProductOptionsDatabase
    {
        private static readonly string DATABASE_NAME = "productoption";

        protected override void Update(ProductOption productOption)
        {
            ExecuteNonQuery($"update productoption set name = '{productOption.Name}', description = '{productOption.Description}' where id = '{productOption.Id}'");
        }

        protected override void Create(ProductOption productOption)
        {
            ExecuteNonQuery($"insert into productoption (id, productid, name, description) values ('{productOption.Id}', '{productOption.ProductId}', '{productOption.Name}', '{productOption.Description}')");
        }

        public void DeleteOptionsGivenProductId(Guid productId)
        {
            var options = GetProductOptions(productId);
            foreach (var option in options)
            {
                Delete(option.Id);
            }
        }

        public List<ProductOption> GetProductOptions(Guid productId)
        {
            return GetMultiple($"select * from productoption where productid = '{productId}'");
        }

        protected override string GetDatabaseName()
        {
            return DATABASE_NAME;
        }

        protected override Guid GetId(ProductOption model)
        {
            return model.Id;
        }

        protected override ProductOption MapModel(SqlDataReader rdr)
        {
            return new ProductOption
            {
                Id = Guid.Parse(rdr["Id"].ToString()),
                ProductId = Guid.Parse(rdr["ProductId"].ToString()),
                Name = rdr["Name"].ToString(),
                Description = (DBNull.Value == rdr["Description"]) ? null : rdr["Description"].ToString()
            };
        }
    }
}