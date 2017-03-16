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
    public class ProductsDatabase : IProductsDatabase
    {

        public List<Product> GetAll()
        {
            var products = new List<Product>();
            var rdr = ExecuteReader($"select * from product");

            while (rdr.Read())
            {
                products.Add(new Product { Id = Guid.Parse(rdr["id"].ToString()), Name = rdr["Name"].ToString(), Description = (DBNull.Value == rdr["Description"]) ? null : rdr["Description"].ToString(), Price = decimal.Parse(rdr["Price"].ToString()), DeliveryPrice = decimal.Parse(rdr["DeliveryPrice"].ToString()) });
            }

            return products;
        }

        public Product Get(Guid id)
        {
            var rdr = ExecuteReader($"select * from product where id = '{id}'");
            if (!rdr.Read())
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            return new Product { Id = Guid.Parse(rdr["Id"].ToString()),
                Name = rdr["Name"].ToString(),
                Description = (DBNull.Value == rdr["Description"]) ? null : rdr["Description"].ToString(),
                Price = decimal.Parse(rdr["Price"].ToString()),
                DeliveryPrice = decimal.Parse(rdr["DeliveryPrice"].ToString())
            };
        }


        public void Delete(Guid id)
        {
            ExecuteReader($"delete from product where id = '{id}'");
        }

        public void Save(Product product)
        {
            var id = product.Id;

            if (AlreadyInDatabase(product))
            {
                Update(product);
            }
            else
            {
                Create(product);
            }
        }

        private Boolean AlreadyInDatabase(Product product)
        {
            var id = product.Id;
            var rdr = ExecuteReader($"select * from product where id = '{id}'");

            return rdr.Read();
        }

        private void Update(Product product)
        {
            ExecuteNonQuery($"update product set name = '{product.Name}', description = '{product.Description}', price = {product.Price}, deliveryprice = {product.DeliveryPrice} where id = '{product.Id}'");
        }

        private void Create(Product product)
        {
            ExecuteNonQuery($"insert into product (id, name, description, price, deliveryprice) values ('{product.Id}', '{product.Name}', '{product.Description}', {product.Price}, {product.DeliveryPrice})");
        }

        private SqlDataReader ExecuteReader(String cmdString)
        {
            var conn = Helpers.NewConnection();
            var cmd = new SqlCommand(cmdString, conn);
            conn.Open();

            return cmd.ExecuteReader();
        }

        private void ExecuteNonQuery(String cmdString)
        {
            var conn = Helpers.NewConnection();
            var cmd = new SqlCommand(cmdString, conn);

            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        public List<Product> SearchByName(string name)
        {
            var products = new List<Product>();
            var rdr = ExecuteReader($"select * from product where lower(name) like '%{name.ToLower()}%'");

            while (rdr.Read())
            {
                products.Add(new Product { Id = Guid.Parse(rdr["id"].ToString()), Name = rdr["Name"].ToString(), Description = (DBNull.Value == rdr["Description"]) ? null : rdr["Description"].ToString(), Price = decimal.Parse(rdr["Price"].ToString()), DeliveryPrice = decimal.Parse(rdr["DeliveryPrice"].ToString()) });
            }

            return products;
        }


    }
}