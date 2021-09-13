using System;
using System.Collections.Generic;
using System.Linq;
using Shops.Tools;

namespace Shops
{
    public class Shop
    {
        private List<Product> _products = new List<Product>();

        public Shop(string name, int shopId)
        {
            Name = name;
            Id = new ShopId(shopId);
        }

        public string Name { get; }
        public ShopId Id { get; }

        public Product RegisterProduct(string name)
        {
            if (_products.Exists(product => product.Name == name))
                throw new ShopManagerException($"The product {name} is already registered.");

            var product = new Product(name);
            _products.Add(product);
            return product;
        }

        public Product FindProduct(string name)
        {
            return _products.Find(product => product.Name == name);
        }

        public Product GetProduct(string name)
        {
            Product product = FindProduct(name);
            if (product == null)
                throw new ShopManagerException($"The product {name} is not found.");
            return product;
        }

        public void MakeSupply(Supply supply)
        {
            foreach (ProductSupply productSupply in supply.ProductSupplies)
            {
                Product product = GetProduct(productSupply.ProductName);
                product.Worth = productSupply.Worth;
                product.Quantity += productSupply.Quantity;
            }
        }

        public int CalculateTotalCost(Purchase purchase)
        {
            int totalCost = 0;
            foreach (ProductPurchase productPurchase in purchase.ProductPurchases)
            {
                totalCost += productPurchase.Quantity * GetProduct(productPurchase.ProductName).Worth;
            }

            return totalCost;
        }

        public bool AreProductsEnough(Purchase purchase)
        {
            foreach (ProductPurchase productPurchase in purchase.ProductPurchases)
            {
                Product product = GetProduct(productPurchase.ProductName);
                if (product.Quantity < productPurchase.Quantity)
                    return false;
            }

            return true;
        }

        public void BuyProducts(Purchase purchase)
        {
            int totalCost = CalculateTotalCost(purchase);
            if (totalCost > purchase.Customer.Balance)
                throw new ShopManagerException("Not enough money to buy.");
            purchase.Customer.Balance -= totalCost;

            if (!AreProductsEnough(purchase))
                throw new ShopManagerException("There are not enough products.");

            foreach (ProductPurchase productPurchase in purchase.ProductPurchases)
            {
                Product product = GetProduct(productPurchase.ProductName);
                product.Quantity -= productPurchase.Quantity;
            }
        }
    }
}