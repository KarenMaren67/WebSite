using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Images;
using Microsoft.AspNetCore.Mvc;

namespace Host.Controllers
{
    public class ImagesController : Controller
    {
		public ImagesController(IImageService imageService)
		{
			_imageService = imageService;
		}

		private readonly IImageService _imageService;

		public async Task<IActionResult> Index() => View(await _imageService.ReadAsync(null));

		public async Task<IActionResult> Details(Guid id) => View(await _imageService.ReadAsync(id));
	}
}