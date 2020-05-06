using System;
using System.Collections.Generic;

namespace Application
{
	public class PagerOutModel<TModel>:PagerInput
	{
		public int AllItemsCount { get; set; }
		public IReadOnlyList<TModel> Items { get; set; }

		public int PageCount => (int)Math.Ceiling(AllItemsCount / (double)CountItemPerPage) ;
	}
}
