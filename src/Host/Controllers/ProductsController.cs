using System;
using System.Threading.Tasks;
using Application.Products;
using Application.Products.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Host.Controllers
{
	public class ProductsController : Controller
	{
		public ProductsController(IProductService productService)
		{
			_productService = productService;
		}

		private readonly IProductService _productService;

		public async Task<IActionResult> Index(ReadProduct model) => View(await _productService.GetFiltredAsync(model));

		public async Task<IActionResult> Details(Guid id) => View(await _productService.ReadAsync(id));
	}
}