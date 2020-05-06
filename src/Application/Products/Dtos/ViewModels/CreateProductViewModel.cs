using System.Collections.Generic;
using Application.Categories.Dtos;

namespace Application.Products.Dtos.ViewModels
{
	public class CreateProductViewModel
	{
		public IReadOnlyList<CategoryDto> Categories { get; set; }

		public CreateProduct Product { get; set; }
	}
}
