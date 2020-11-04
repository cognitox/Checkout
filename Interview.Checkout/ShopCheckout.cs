using Interview.Checkout.Data.Interfaces;
using Interview.Checkout.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Interview.Checkout
{
    public class ShopCheckout : ICheckout
    {
        private readonly IRepository<Product> _catalog;
        private readonly IRepository<Discount> _discounts;
        public List<string> ScannedProducts { get; private set; }

        public ShopCheckout(IRepository<Product> products, IRepository<Discount> discounts)
        {
            _catalog = products;
            _discounts = discounts;
            ScannedProducts = new List<string>();
        }

        public ICheckout Scan(String sku)
        {
            if (!String.IsNullOrEmpty(sku))
            {
                if (_catalog.Any(product => product.SKU == sku))
                {
                    ScannedProducts.Add(sku);
                }                                    
            }
            return this;
        }       
    }
}
