using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Application.Products.Dtos.Cart;

namespace Application.Orders.Dtos
{
	public class CreateOrder
	{
		[Display(Name = "Имя")]
		public string Name { get; set; }

		[Display(Name = "Номер телефона")]
		[Required]
		public string Phone { get; set; }

		public decimal Price { get; set; }
		
		public CartProduct[] CartProducts { get; set; }
	}
}
