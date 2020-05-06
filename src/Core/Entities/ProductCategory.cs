using System;

namespace Core.Entities
{
	public class ProductCategory
	{
		public Product Product { get; set; }
		public Guid ProductId { get; set; }

		public Category Category { get; set; }
		public Guid CategoryId { get; set; }

	}
}
