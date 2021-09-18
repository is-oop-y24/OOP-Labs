using System;

namespace Shops
{
    public class Product
    {
        public Product(string name, ProductId id)
        {
            Name = name;
            Id = id;
            Quantity = 0;
        }

        public int Worth { get; set; }
        public int Quantity { get; internal set; }
        public string Name { get; }
        public ProductId Id { get; }
    }
}