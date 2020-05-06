using Application.Products.Dtos;
using Application.Sellers.Dtos;
using System;
using System.Collections.Generic;
using Application.Users.Dtos;

namespace Application.Orders.Dtos
{
	public class OrderDto
	{
		public Guid Id { get; set; }
		public IEnumerable<ProductDto> SelectedProducts { get; set; }

		public UserDto User { get; set; }

		public SellerDto Seller { get; set; }
	}
}
