using Interview.Checkout.Data;
using Interview.Checkout.Data.Entities;
using Interview.Checkout.Infrastructure;
using System;
using Xunit;

namespace Interview.Checkout.Test
{
    public class CheckoutTest
    {
        private ICheckout checkout;
        private FakeDbContext _dbContext = new FakeDbContext();

        public CheckoutTest()
        {
            var products = new Repository<Product>(_dbContext);
            var discounts = new Repository<Discount>(_dbContext);

            checkout = new ShopCheckout(products, discounts);

            products.Add(new Product { SKU = "A99", Price = 50 });
            products.Add(new Product { SKU = "B15", Price = 30 });
            products.Add(new Product { SKU = "C40", Price = 20 });

            discounts.Add(new Discount { SKU = "A99", Quantity = 3, Value = 20 });
            discounts.Add(new Discount { SKU = "B15", Quantity = 2, Value = 15 });          

        }

        [Fact]
        public void No_items_returns_zero()
        {
            checkout.Scan("A99");
            Assert.Single(checkout.ScannedProducts);
        }
    }
}
