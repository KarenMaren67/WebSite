using System;
using Core.Infrastructure;

namespace Core.Entities
{
	public class Seller : Entity<Guid>
	{
		public string Email { get; set; }
		public string Name { get; set; }
	}
}
