using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Products.Dtos;

namespace Host.Extensions
{
	public static class ProductExtensions
	{
		public static string GetProductDimensions(this ProductDto product)
		{
			return $"Размеры(ШхГхВ): {product.Width}x{product.Depth}x{product.Height}";
		}

	}
}
