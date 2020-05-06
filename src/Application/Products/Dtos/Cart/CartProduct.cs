using System;

namespace Application.Products.Dtos.Cart
{
	public class CartProduct
	{
		public Guid ProductId { get; set; }
		public int Count { get; set; }
	}
}