using System;
using Shops.Tools;

namespace Shops
{
    public class Customer
    {
        private int _balance;

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

        public int Balance
        {
            get => _balance;
            set
            {
                if (value < 0)
                    throw new ShopManagerException("Customer balance cannot be a negative number.");
                _balance = value;
            }
        }
        
        public Purchase CurrentPurchase { get; private set; }

        public Purchase NewPurchase()
        {
            CurrentPurchase = new Purchase(this);
            return CurrentPurchase;
        }
    }
}