using System;
using Shops.Tools;

namespace Shops
{
    public class Customer
    {
        public Customer(string name)
        {
            Name = name;
            Balance = 0;
        }

        public Customer(string name, int balance)
        {
            if (balance < 0)
                throw new ShopManagerException("Customer balance cannot be a negative number.");
            
            Name = name;
            Balance = balance;
        }

        public string Name { get; }
        public int Balance { get; set; }
    }
}