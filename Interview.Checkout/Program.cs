using Interview.Checkout.Data;
using Interview.Checkout.Data.Entities;
using Interview.Checkout.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interview.Checkout
{
    class Program
    {
        static void Main(string[] args)
        {
            //Prepare data
            FakeDbContext _dbContext = new FakeDbContext();
            var products = new Repository<Product>(_dbContext);
            var discounts = new Repository<Discount>(_dbContext);

            ICheckout checkout = new ShopCheckout(products, discounts);

            products.Add(new Product { SKU = "A99", Price = 0.50m });
            products.Add(new Product { SKU = "B15", Price = 0.30m });
            products.Add(new Product { SKU = "C40", Price = 0.60m });

            discounts.Add(new Discount { SKU = "A99", Quantity = 3, Value = 1.30m });
            discounts.Add(new Discount { SKU = "B15", Quantity = 2, Value = 0.45m });

            //Test Checkout
            checkout.Scan("A99");
            checkout.Scan("A99");
            checkout.Scan("A99");
            checkout.Scan("A99");
            checkout.Scan("B15");
            Console.WriteLine($"Total cost of shopping is {checkout.Total()}");
        }
    }
}
