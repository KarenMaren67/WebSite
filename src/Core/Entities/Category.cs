using System;
using System.Collections.Generic;
using Core.Infrastructure;

namespace Core.Entities
{
	public class Category: Entity<Guid>
	{
		public string Name { get; set; }

		public ICollection<ProductCategory> ProductCategories { get; set; }

		public Guid? TopCategoryId { get; set; }
		public Category TopCategory { get; set; }


		public ICollection<Category> SubCategories { get; set; }
	}
}
