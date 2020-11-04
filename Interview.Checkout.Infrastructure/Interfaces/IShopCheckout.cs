using System;
using System.Collections.Generic;

namespace Interview.Checkout.Infrastructure
{
    public interface ICheckout
    {
        List<string> ScannedProducts { get; }
        ICheckout Scan(String scan);
    }
}
