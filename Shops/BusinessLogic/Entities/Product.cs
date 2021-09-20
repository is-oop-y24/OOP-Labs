using System;
using Shops.Tools;

namespace Shops
{
    public class Product
    {
        public Product(string name, ProductId id)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ShopManagerException("Product name cannot be empty");
            Name = name;
            Id = id;
            Quantity = 0;
        }

        public int Worth { get; internal set; }
        public int Quantity { get; internal set; }
        public string Name { get; }
        public ProductId Id { get; }
    }
}