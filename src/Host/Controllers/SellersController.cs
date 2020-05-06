using System;
using System.Threading.Tasks;
using Application.Sellers;
using Microsoft.AspNetCore.Mvc;

namespace Host.Controllers
{
    public class SellersController : Controller
    {
		public SellersController(ISellerService sellerService)
		{
			_sellerService = sellerService;
		}

		private readonly ISellerService _sellerService;

		public async Task<IActionResult> Index() => View(await _sellerService.ReadAsync(null));

		public async Task<IActionResult> Details(Guid id) => View(await _sellerService.ReadAsync(id));
	}
}