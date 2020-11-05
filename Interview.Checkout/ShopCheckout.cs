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
                    Console.WriteLine($"Scanned product {sku}");
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
            decimal total = 0;
            total = ShoppingCart.GroupBy(sku => sku).Sum(g => GetDiscountedTotal(g.Key, g.Count()));
            return total;
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
              
        /// <summary>
        /// Get Total Price for all product of same type with discount applied
        /// </summary>
        /// <param name="sku"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        private decimal GetDiscountedTotal(string sku, int count)
        {
            decimal total = 0;

            var discount = _discounts.GetAll(d => d.SKU == sku).FirstOrDefault();
            if (discount == null)
            {
                return count * PriceFor(sku);
            }
            var offerAppliedItemsTotal = (count / discount.Quantity) * discount.Value;
            var offerNotAppliedItemsTotal = (count % discount.Quantity) * PriceFor(sku);
            total = offerAppliedItemsTotal + offerNotAppliedItemsTotal;
            return total;
        }
    }
}
