using Application.Categories.Dtos;
using Application.Images.Dtos;
using Application.Orders.Dtos;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Application.Products.Dtos
{
	public class CreateProduct
	{
		public CreateProduct()
		{
			Images = new List<IFormFile>();
		}

		[Display(Name = "Артикул")]
		[Required]
		public string Articul { get; set; }

		[Display(Name = "Изображения")]
		[Required]
		public IReadOnlyList<IFormFile> Images { get; set; }

		[Display(Name = "Наименование продукта")]
		[Required]
		public string Name { get; set; }

		[Display(Name = "Категория")]
		public CategoryDto Category { get; set; }

		[Display(Name = "Ширина")]
		public int Width { get; set; }

		[Display(Name = "Высота")]
		public int Height { get; set; }

		[Display(Name = "Глубина")]
		public int Depth { get; set; }

		[Display(Name = "Цвет")]
		public string Color { get; set; }

		[Display(Name = "Наличие на складе")]
		public string Status { get; set; }

		[Display(Name = "Оптовая цена в $")]
		[Required]
		public decimal WholesalePrice { get; set; }

		[Display(Name = "Розничная цена в $")]
		[Required]
		public decimal RetailPrice { get; set; }

		[Display(Name = "Количество на складе")]
		public int Count { get; set; }
	}
}