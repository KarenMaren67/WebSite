using Core.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Entities
{
	public class Setting: Entity<Guid>
	{
		public string Key { get; set; }

		public string Value { get; set; }
	}
}
