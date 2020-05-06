using System.Collections.Generic;
using Application.Categories.Dtos;

namespace Application.Products.Dtos.ViewModels
{
	public class ProductListForAdminViewModel
	{
		public IReadOnlyList<CategoryDto> Categories { get; set; }

		public IReadOnlyList<ProductDto> Products { get; set; }
	}
}
