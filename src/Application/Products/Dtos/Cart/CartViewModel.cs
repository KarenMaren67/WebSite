using System.Collections.Generic;
using System.Linq;

namespace Application.Products.Dtos.Cart
{
	public class CartViewModel
	{
		public CartViewModel()
		{
			Products = new List<CartProductViewModel>();
		}

		public IReadOnlyList<CartProductViewModel> Products { get; set; }

		public decimal Price => Products.Sum(x => x.Price);
	}
}