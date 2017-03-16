using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Net;
using System.Web.Http;
using refactor_me.Models;

namespace refactor_me.Services
{
    public abstract class DatabaseOperations<T>
    {
        protected List<T> GetMultiple(string cmd)
        {
            var rdr = ExecuteReader(cmd);
            List<T> items = new List<T>();
            while (rdr.Read())
            {
                items.Add(MapModel(rdr));
            }

            return items;
        }

        public void Save(T model)
        {

            if (AlreadyInDatabase(GetId(model)))
            {
                Update(model);
            }
            else
            {
                Create(model);
            }
        }

        public T Get(Guid id)
        {
            var rdr = ExecuteReader($"select * from " + GetDatabaseName() +" where id = '" + id.ToString() + "'");
            if (!rdr.Read())
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            return MapModel(rdr);
        }


        public void Delete(Guid id)
        {
            ExecuteReader($"delete from " + GetDatabaseName() + " where id = '" + id.ToString() + "'");
        }

        protected bool AlreadyInDatabase(Guid id)
        {
            var rdr = ExecuteReader($"select * from " + GetDatabaseName() + " where id = '" + id.ToString() + "'");
            return rdr.Read();
        }

        protected SqlDataReader ExecuteReader(String cmdString)
        {
            var conn = Helpers.NewConnection();
            var cmd = new SqlCommand(cmdString, conn);
            conn.Open();

            return cmd.ExecuteReader();
        }

        protected void ExecuteNonQuery(String cmdString)
        {
            var conn = Helpers.NewConnection();
            var cmd = new SqlCommand(cmdString, conn);

            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
        }


        protected abstract string GetDatabaseName();

        protected abstract void Create(T model);

        protected abstract void Update(T model);

        protected abstract Guid GetId(T model);

        protected abstract T MapModel(SqlDataReader rdr);
    }
}