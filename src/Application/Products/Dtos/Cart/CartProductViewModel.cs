namespace Application.Products.Dtos.Cart
{
	public class CartProductViewModel
	{
		public ProductDto Product { get; set; }
		public int Count { get; set; }

		public decimal Price => Product.RetailPriceInRubles * Count;
	}
}