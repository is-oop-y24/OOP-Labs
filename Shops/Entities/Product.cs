using System;

namespace Shops
{
    public class Product
    {
        public Product(string name)
        {
            Name = name;
            Quantity = 0;
        }

        public int Worth { get; set; }
        public int Quantity { get; set; }
        public string Name { get; }
    }
}