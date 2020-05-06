using System;
using System.Collections.Generic;
using Application.Constants;
using Application.Categories.Dtos;
using Application.Images.Dtos;
using Application.Orders.Dtos;

namespace Application.Products.Dtos
{
	public class ProductDto
	{
		public Guid Id { get; set; }
		//Артикул
		public string Articul { get; set; }
		//изображения
		public IReadOnlyList<ImageDto> Images { get; set; }
		//Наименование
		public string Name { get; set; }
		//ширина
		public int Width { get; set; }
		//Высота
		public int Height { get; set; }
		//Глубина
		public int Depth { get; set; }
		//Заказы
		public IEnumerable<OrderDto> Orders { get; set; }
		//Цвет
		public string Color { get; set; }
		//Наличие
		public string Status { get; set; }
		//Оптовая цена
		public decimal WholesalePrice { get; set; }
		//Розничная цена
		public decimal RetailPrice { get; set; }
		//Количество
		public int Count { get; set; }
		//todo
		public decimal RetailPriceInRubles => Math.Round(RetailPrice * Constant.DollarRate, 2, MidpointRounding.ToEven);
		//Категории
		public CategoryDto Category { get; set; }
	}
}