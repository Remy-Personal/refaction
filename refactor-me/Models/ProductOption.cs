using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace refactor_me.Models
{
    public class ProductOption
    {
        public Guid Id { get; set; }

        public Guid ProductId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            ProductOption castedObj = (ProductOption)obj;

            return Id.Equals(castedObj.Id)
                && ProductId.Equals(castedObj.ProductId)
                && Name.Equals(castedObj.Name)
                && Description.Equals(castedObj.Description);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode() * ProductId.GetHashCode() * Name.GetHashCode() * Description.GetHashCode();
        }
    }
}