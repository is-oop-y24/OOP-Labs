using System;

namespace Shops
{
    public class Customer
    {
        public Customer(string name)
        {
            throw new NotImplementedException();
        }

        public Customer(string name, int balance) 
            : this(name)
        {
            throw new NotImplementedException();
        }

        public int Balance { get; set; }
    }
}