using System;

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
            Name = name;
            Balance = balance;
        }

        public string Name { get; }
        public int Balance { get; set; }
    }
}