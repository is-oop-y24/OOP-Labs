using System;
using System.Collections.Generic;
using System.Linq;
using Shops.Tools;

namespace Shops
{
    public class Shop
    {
        private List<Product> _products = new List<Product>();

        public Shop(string name, int shopId, Address shopAddress)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ShopManagerException("Shop name cannot be empty.");
            Name = name;
            Id = new ShopId(shopId);
            Address = shopAddress;
        }

        public string Name { get; }
        public ShopId Id { get; }
        public Address Address { get; }

        public Product RegisterProduct(string name, ProductId productId)
        {
            var product = new Product(name, productId);
            _products.Add(product);
            return product;
        }

        public Product FindProduct(string name)
        {
            return _products.Find(product => product.Name == name);
        }

        public Product GetProduct(ProductId productId)
        {
            Product product = _products.Find(product => product.Id.GetId() == productId.GetId());
            if (product == null)
                throw new ShopManagerException($"The product {productId} is not found.");
            return product;
        }

        public void MakeSupply(Supply supply)
        {
            foreach (ProductSupply productSupply in supply.ProductSupplies)
            {
                Product product = GetProduct(productSupply.ProductId);
                product.Worth = productSupply.Worth;
                product.Quantity += productSupply.Quantity;
            }
        }

        public int CalculateTotalCost(Purchase purchase)
        {
            int totalCost = 0;
            foreach (ProductPurchase productPurchase in purchase.ProductPurchases)
            {
                totalCost += productPurchase.Quantity * GetProduct(productPurchase.ProductId).Worth;
            }

            return totalCost;
        }

        public bool AreProductsEnough(Purchase purchase)
        {
            foreach (ProductPurchase productPurchase in purchase.ProductPurchases)
            {
                try
                {
                    Product product = GetProduct(productPurchase.ProductId);
                    if (product.Quantity < productPurchase.Quantity)
                        return false;
                }
                catch (ShopManagerException)
                {
                    return false;
                }
            }

            return true;
        }

        public void BuyProducts(Purchase purchase)
        {
            int totalCost = CalculateTotalCost(purchase);
            if (totalCost > purchase.Customer.Balance)
                throw new ShopManagerException("Not enough money to buy.");
            if (!AreProductsEnough(purchase))
                throw new ShopManagerException("There are not enough products.");

            purchase.Customer.Balance -= totalCost;
            foreach (ProductPurchase productPurchase in purchase.ProductPurchases)
            {
                Product product = GetProduct(productPurchase.ProductId);
                product.Quantity -= productPurchase.Quantity;
            }
        }
    }
}