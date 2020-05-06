using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Orders;
using Application.Orders.Dtos;
using Application.Products;
using Application.Products.Dtos.Cart;
using Host.Constants;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Host.Controllers
{
    public class OrdersController : Controller
    {
		private readonly IOrderService _orderService;

		public OrdersController(IOrderService orderService,IProductService productService)
		{
			_orderService = orderService;
		}

		public IActionResult Create() => View();

		[HttpPost]
		public async Task<IActionResult> Create(CreateOrder model)
		{
			if (ModelState.IsValid)
			{
				if (Request.Cookies.TryGetValue(Cookie.CartCookieName, out string value))
				{
					model.CartProducts = JsonConvert.DeserializeObject<CartProduct[]>(value);

					if (model.CartProducts.Any())
					{
						await _orderService.CreateAsync(model);
						Response.Cookies.Delete(Cookie.CartCookieName);
						return RedirectToAction(nameof(Confirmed));
					}

					ModelState.AddModelError("CartProducts","Не может быть пустым.");
				}
			}

			return View(model);
		}

		public IActionResult Confirmed() => View();
	}
}