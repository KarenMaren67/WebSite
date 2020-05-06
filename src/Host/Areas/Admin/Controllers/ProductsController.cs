using System;
using System.Threading.Tasks;
using Application.Categories;
using Application.Categories.Dtos;
using Application.Products;
using Microsoft.AspNetCore.Authorization;
using Application.Products.Dtos;
using Application.Products.Dtos.ViewModels;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;

namespace Host.Areas.Admin.Controllers
{
	[Authorize(Roles = "Admin")]
	[Area("Admin")]
	public class ProductsController : Controller
	{
		private readonly IProductService _productService;
		private readonly ICategoryService _categoryService;
		private readonly IMapper _mapper;

		public ProductsController(IProductService productService, ICategoryService categoryService, IMapper mapper)
		{
			_productService = productService;
			_categoryService = categoryService;
			_mapper = mapper;
		}

		public async Task<IActionResult> Add() => View(new CreateProductViewModel
		{
			Categories = await _categoryService.ReadAsync(new ReadCategory()),
			Product = new CreateProduct()
		});

		[HttpPost]
		public async Task<IActionResult> Add(CreateProductViewModel createProductViewModel)
		{
			if (ModelState.IsValid)
			{
				await _productService.AddImagesToProductAsync(createProductViewModel.Product);
				return RedirectToAction("Index", "Home", new { area = "Admin" });
			}

			createProductViewModel.Categories = await _categoryService.ReadAsync(new ReadCategory());
			return View(createProductViewModel);
		}

		public async Task<IActionResult> Edit(Guid id) => View(new UpdateProductViewModel
		{
			Categories = await _categoryService.ReadAsync(new ReadCategory()),
			Product = _mapper.Map<UpdateProduct>( await _productService.ReadAsync(id))
		});

		[HttpPost]
		public async Task<IActionResult> Edit(UpdateProductViewModel model)
		{
			await _productService.UpdateAsync(model.Product);
			return RedirectToAction("Index", "Home", new { area = "Admin" });
		}

		public async Task<IActionResult> Delete(Guid id) => View(await _productService.ReadAsync(id));

		[HttpPost]
		public async Task<IActionResult> Delete(ProductDto product)
		{
			await _productService.DeleteAsync(product.Id);
			return RedirectToAction("Index", "Home", new { area = "Admin" });
		}
	}
}