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
            NewPurchase();
        }

        public string Name { get; }

        public Purchase CurrentPurchase { get; private set; }

        public int Balance
        {
            get => _balance;
            internal set
            {
                if (value < 0)
                    throw new ShopManagerException("Customer balance cannot be a negative number.");
                _balance = value;
            }
        }

        public Purchase NewPurchase()
        {
            CurrentPurchase = new Purchase(this);
            return CurrentPurchase;
        }
    }
}