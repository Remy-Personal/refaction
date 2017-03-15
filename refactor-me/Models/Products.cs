using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Newtonsoft.Json;

namespace refactor_me.Models
{
    public class Products
    {
        public List<Product> Items { get; private set; }

        public Products(List<Product> items)
        {
            this.Items = items;
        }
    }
}