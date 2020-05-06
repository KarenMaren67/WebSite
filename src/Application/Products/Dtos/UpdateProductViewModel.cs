using Application.Categories.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Products.Dtos
{
	public class UpdateProductViewModel
	{
		public IReadOnlyList<CategoryDto> Categories { get; set; }

		public UpdateProduct Product { get; set; }
	}
}
