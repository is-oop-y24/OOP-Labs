using System;
using Shops.Tools;

namespace Shops
{
    public class Customer
    {
        private int _balance;
        private Purchase _currentPurchase;

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

        public Purchase CurrentPurchase
        {
            get => _currentPurchase ?? throw new ShopManagerException("Purchase is not created;");
            private set => _currentPurchase = value;
        }

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