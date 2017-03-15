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
    public class ProductOptionsDatabase : IProductOptionsDatabase
    {

        public ProductOption Get(Guid id)
        {
            var rdr = ExecuteReader($"select * from productoption where id = '{id}'");
            if (!rdr.Read())
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            return new ProductOption {
                Id = Guid.Parse(rdr["Id"].ToString()),
                ProductId = Guid.Parse(rdr["ProductId"].ToString()),
                Name = rdr["Name"].ToString(),
                Description = (DBNull.Value == rdr["Description"]) ? null : rdr["Description"].ToString()
            };
        }

        public void Save(ProductOption productOption)
        {

            var id = productOption.Id;

            if (AlreadyInDatabase(productOption))
            {
                Update(productOption);
            }
            else
            {
                Create(productOption);
            }
        }

        public void Delete(Guid id)
        {
            ExecuteReader($"delete from productoption where id = '{id}'");
        }

        private Boolean AlreadyInDatabase(ProductOption productOption)
        {
            var id = productOption.Id;
            var rdr = ExecuteReader($"select * from productoption where id = '{id}'");

            return rdr.Read();
        }

        private void Update(ProductOption productOption)
        {
            ExecuteNonQuery($"update productoption set name = '{productOption.Name}', description = '{productOption.Description}' where id = '{productOption.Id}'");
        }

        private void Create(ProductOption productOption)
        {
            ExecuteNonQuery($"insert into productoption (id, productid, name, description) values ('{productOption.Id}', '{productOption.ProductId}', '{productOption.Name}', '{productOption.Description}')");
        }

        public void DeleteOptionsGivenProductId(Guid productId)
        {
            var options = GetProductOptions(productId);
            foreach (var option in options.Items)
            {
                Delete(option.Id);
            }
        }

        public ProductOptions GetProductOptions(Guid productId)
        {
            List<ProductOption> items = new List<ProductOption>();
            var rdr = ExecuteReader($"select * from productoption where productid = '{productId}'");

            while (rdr.Read())
            {
                items.Add(new ProductOption { Id = Guid.Parse(rdr["Id"].ToString()), ProductId = Guid.Parse(rdr["ProductId"].ToString()), Name = rdr["Name"].ToString(), Description = (DBNull.Value == rdr["Description"]) ? null : rdr["Description"].ToString() });

            }

            return new ProductOptions(items);

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
    }
}