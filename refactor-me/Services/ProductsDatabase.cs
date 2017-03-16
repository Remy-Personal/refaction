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
    public class ProductsDatabase : DatabaseOperations<Product>, IProductsDatabase
    {
        private static readonly string DATABASE_NAME = "product";

        public List<Product> GetAll()
        {
            return GetMultiple($"select * from product");
        }

        protected override void Update(Product model)
        {
            ExecuteNonQuery($"update product set name = '{model.Name}', description = '{model.Description}', price = {model.Price}, deliveryprice = {model.DeliveryPrice} where id = '{model.Id}'");
        }

        protected override void Create(Product model)
        {
            ExecuteNonQuery($"insert into product (id, name, description, price, deliveryprice) values ('{model.Id}', '{model.Name}', '{model.Description}', {model.Price}, {model.DeliveryPrice})");
        }

        public List<Product> SearchByName(string name)
        {
            var products = new List<Product>();
            var rdr = ExecuteReader($"select * from product where lower(name) like '%{name.ToLower()}%'");

            while (rdr.Read())
            {          
                products.Add(MapModel(rdr));
            }

            return products;
        }

        protected override string GetDatabaseName()
        {
            return DATABASE_NAME;
        }

        protected override Guid GetId(Product model)
        {
            return model.Id;
        }

        protected override Product MapModel(SqlDataReader rdr)
        {
            return new Product
            {
                Id = Guid.Parse(rdr["Id"].ToString()),
                Name = rdr["Name"].ToString(),
                Description = (DBNull.Value == rdr["Description"]) ? null : rdr["Description"].ToString(),
                Price = decimal.Parse(rdr["Price"].ToString()),
                DeliveryPrice = decimal.Parse(rdr["DeliveryPrice"].ToString())
            };
        }
    }
}