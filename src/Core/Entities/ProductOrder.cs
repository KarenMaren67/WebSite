using System;

namespace Core.Entities
{
	public class ProductOrder
	{
		public Guid ProductId { get; set; }
		public Product Product { get; set; }

		public Guid OrderId { get; set; }
		public Order Order { get; set; }

		public int ProductCount { get; set; }
	}
}
