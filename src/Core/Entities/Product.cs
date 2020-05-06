using System;
using System.Collections.Generic;
using Core.Infrastructure;

namespace Core.Entities
{
	public class Product : Entity<Guid>
	{
		//Артикул
		public string Articul { get; set; }

		//Наименование
		public string Name { get; set; }

		//ширина
		public int Width { get; set; }

		//Высота
		public int Height { get; set; }

		//Глубина
		public int Depth { get; set; }

		//изображения
		public ICollection<Image> Images { get; set; }

		//Заказы
		public ICollection<ProductOrder> ProductOrders { get; set; }

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

		//Категории
		public ICollection<ProductCategory> ProductCategories { get; set; }
	}
}
