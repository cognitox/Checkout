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
        public List<string> ShoppingCart { get; private set; }

        public ShopCheckout(IRepository<Product> products, IRepository<Discount> discounts)
        {
            _catalog = products;
            _discounts = discounts;
            ShoppingCart = new List<string>();
        }

        public ICheckout Scan(String sku)
        {
            if (!String.IsNullOrEmpty(sku))
            {
                if (_catalog.Any(product => product.SKU == sku))
                {
                    ShoppingCart.Add(sku);
                }                                    
            }
            return this;
        }

        /// <summary>
        /// Calculate Total for  Shopping Cart
        /// </summary>
        /// <returns></returns>
        public decimal Total()
        {
            return ShoppingCart.Sum(sku => PriceFor(sku));
        }
        /// <summary>
        /// Get Price for Product
        /// </summary>
        /// <param name="sku"></param>
        /// <returns></returns>
        private decimal PriceFor(string sku)
        {
            return _catalog.Single(p => p.SKU == sku).Price;
        }        
    }
}
