using System;
using System.Collections.Generic;

namespace Interview.Checkout.Infrastructure
{
    public interface ICheckout
    {
        List<string> ShoppingCart { get; }
        ICheckout Scan(String scan);

        decimal Total();
    }
}
