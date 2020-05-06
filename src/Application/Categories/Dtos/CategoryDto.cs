using System;
using System.Collections.Generic;

namespace Application.Categories.Dtos
{
	public class CategoryDto
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public IReadOnlyList<CategoryDto> SubCategories { get; set; }
		public bool IsSelected { get; set; }
	}
}
