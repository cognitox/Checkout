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

            products.Add(new Product { SKU = "A99", Price = 0.50m });
            products.Add(new Product { SKU = "B15", Price = 0.30m });
            products.Add(new Product { SKU = "C40", Price = 0.60m });

            discounts.Add(new Discount { SKU = "A99", Quantity = 3, Value = 1.30m });
            discounts.Add(new Discount { SKU = "B15", Quantity = 2, Value = 0.45m });          

        }

        [Fact]
        public void Scanned_One_Product_Single_Returns_True()
        {
            checkout.Scan("A99");
            Assert.Single(checkout.ShoppingCart);
        }

        [Fact]
        public void Scanned_Single_Product_Expect_Correct_Price()
        {
            checkout.Scan("A99");
            Assert.Equal(.50m, checkout.Total());
        }

        [Fact]
        public void Scan_One_More_Than_Discounted_Quantity_And_Expect_Correct_Price()
        {
            checkout.Scan("A99");
            checkout.Scan("A99");
            checkout.Scan("A99");
            checkout.Scan("A99");
            checkout.Scan("B15");
            Assert.Equal(2.10m, checkout.Total());
        }

        [Fact]
        public void Scan_Combination_Of_Discounted_And_NonDiscounted_Item_And_Expect_Correct_Price()
        {
            checkout.Scan("A99");
            checkout.Scan("A99");
            checkout.Scan("A99");
            checkout.Scan("C40");
            checkout.Scan("B15");
            Assert.Equal(2.20m, checkout.Total());
        }

        [Fact]
        public void No_items_returns_zero()
        {
            Assert.Equal(0, checkout.Total());
        }
    }
}
