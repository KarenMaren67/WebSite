using System;

namespace Application.Products.Dtos
{
	public class ReadProduct:PagerInput
	{
		public Guid? CategoryId { get; set; }
		public Guid[] ProductsId { get;set; }
	}
}