namespace Interview.Checkout.Infrastructure
{
    public interface IProduct
    {
        string SKU { get; set; }
        decimal Price { get; set; }
    }
}
