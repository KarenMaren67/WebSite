using System;
using System.Threading.Tasks;
using Application.Categories;
using Application.Categories.Dtos;
using Microsoft.AspNetCore.Authorization;
using Host.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace Host.Areas.Admin.Controllers
{
	[Authorize(Roles = "Admin")]
	[Area("Admin")]
    public class CategoriesController : BaseController
	{
		private readonly ICategoryService _categoryService;

		public CategoriesController(ICategoryService categoryService )
		{
			_categoryService = categoryService;
		}

		public async Task<IActionResult> Edit(Guid id)
		{
			var dto = await _categoryService.ReadAsync(id);
			return View(Mapper.Map<UpdateCategory>(dto));
		}

		public async Task<IActionResult> Delete(Guid id) => View(await _categoryService.ReadAsync(id));

		public IActionResult Add() => View();
		
		[HttpPost]
		public async Task<IActionResult> Add(CreateCategory category)
		{
			if (ModelState.IsValid)
			{
				await _categoryService.CreateAsync(category);
				return RedirectToAction("Index", "Home", new {area = "Admin"});
			}

			return View(category);
		}

		[HttpPost]
		public async Task<IActionResult> Edit(UpdateCategory category)
		{
			if (ModelState.IsValid)
			{
				await _categoryService.UpdateAsync(category);
				return RedirectToAction("Index", "Home", new { area = "Admin" });
			}

			return View(category);
		}

		[HttpPost]
		public async Task<IActionResult> Delete(CategoryDto category)
		{
			await _categoryService.DeleteAsync(category.Id);
			return RedirectToAction("Index", "Home", new { area = "Admin" });
		}
	}
}