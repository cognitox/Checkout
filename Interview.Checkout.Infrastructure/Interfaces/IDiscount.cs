namespace Interview.Checkout.Infrastructure
{
    public interface IDiscount
    {
        string SKU { get; set; }
		int Quantity { get; set; }
		decimal Value { get; set; }
    }
}
