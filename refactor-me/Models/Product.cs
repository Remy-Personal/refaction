using System;

namespace refactor_me.Models
{
    public class Product
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public decimal DeliveryPrice { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            Product castedObj = (Product)obj;

            return Id.Equals(castedObj.Id)
                && Name.Equals(castedObj.Name)
                && Description.Equals(castedObj.Description)
                && Price == castedObj.Price
                && DeliveryPrice == castedObj.DeliveryPrice;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode() * Name.GetHashCode() * Description.GetHashCode() * (int) Price * (int) DeliveryPrice;
        }
    }
}