using System.Collections.Generic;
using Application.Categories.Dtos;

namespace Application.Products.Dtos.ViewModels
{
	public class GetFiltredViewModel
	{
		public PagerOutModel<ProductDto> PagerOutModel { get; set; }
		public IReadOnlyList<CategoryDto> MainCategories { get; set; }
		public ReadProduct ReadProduct { get; set; }
		public CategoryDto SelectedMainCategory { get; set; }
	}
}