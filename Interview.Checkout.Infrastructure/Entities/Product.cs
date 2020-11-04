namespace Interview.Checkout.Infrastructure
{
	public class Product : IProduct
	{
		public string SKU { get; set; }
		public decimal Price { get; set; }
	}
}
