using System;
using System.Collections.Generic;
using Core.Infrastructure;

namespace Core.Entities
{
	public class Order : Entity<Guid>
	{
		public Order()
		{
			OrderProducts = new List<ProductOrder>();
		}

		public Seller Seller { get; set; }
		public Guid SellerId { get; set; }

		public User Customer { get; set; }
		public string CustomerId { get; set; }

		public ICollection<ProductOrder> OrderProducts { get; set; }
	}
}
