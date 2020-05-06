using System;
using Core.Infrastructure;

namespace Core.Entities
{
	public class Image : Entity<Guid>
	{
		public string Path { get; set; }
		public Guid ProductId { get; set; }
		public Product Product { get; set; }
	}
}
