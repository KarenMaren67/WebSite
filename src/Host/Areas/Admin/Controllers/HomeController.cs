using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Products;
using Application.Products.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Host.Areas.Admin.Controllers
{
	[Authorize(Roles = "Admin")]
	[Area("Admin")]
	public class HomeController:Controller
	{
		public HomeController( IProductService productService)
		{
			_productService = productService;
		}

		private IProductService _productService;

		public async Task<IActionResult> Index(ReadProduct model) => View( await _productService.GetProductListForAdminViewModelAsync(model));

	}
}
