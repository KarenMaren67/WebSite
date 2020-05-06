using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Application.Categories.Dtos;
using Application.Extensions;
using Application.Products.Dtos;
using Application.Products.Dtos.Cart;
using Application.Products.Dtos.ViewModels;
using AutoMapper;
using Core;
using Core.Entities;
using Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;

namespace Application.Products
{
	internal class ProductService : CrudService<Product, Guid, CreateProduct, ReadProduct, UpdateProduct, ProductDto>, IProductService
	{
		private readonly IRepository<Category, Guid> _categoryRepository;
		private readonly IHostingEnvironment _appEnvironment;

		public ProductService(IRepository<Product, Guid> repository, IMapper mapper, IRepository<Category, Guid> categoryRepository, IHostingEnvironment appEnvironment) :
			base(repository, mapper)
		{
			_categoryRepository = categoryRepository;
			_appEnvironment = appEnvironment;
		}

		public async Task<ProductListForAdminViewModel> GetProductListForAdminViewModelAsync(ReadProduct readProduct)
		{
			return new ProductListForAdminViewModel()
			{
				Categories = Mapper.Map<IReadOnlyList<CategoryDto>>(await _categoryRepository.GetAllAsync()),
				Products = Mapper.Map<IReadOnlyList<ProductDto>>(Read(await Repository.GetAllAsync(), readProduct))
			};
		}

		protected override IQueryable<Product> Read(IQueryable<Product> entities, ReadProduct readEntity)
		{
			return entities.WhereIf(readEntity.CategoryId.HasValue, x => x.ProductCategories.Any(c => c.CategoryId == readEntity.CategoryId))
						   .WhereIf(readEntity.ProductsId != null, x => readEntity.ProductsId.Any(p => p == x.Id))
						   .Include(x => x.Images);
		}

		public async Task<GetFiltredViewModel> GetFiltredAsync(ReadProduct readProduct)
		{
			var products = Read(await Repository.GetAllAsync(), readProduct).Where(p => p.Images.Any());
			var count = products.Count();
			var pagerOutModel = new PagerOutModel<ProductDto>
			{
				AllItemsCount = count,
				CurrentPage = readProduct.CurrentPage,
				CountItemPerPage = readProduct.CountItemPerPage,
				Items = Mapper.Map<IReadOnlyList<ProductDto>>(await products.GetPaged(readProduct).ToListAsync())
			};

			var mainCategoriesDto = await GetMainCategoriesAsync();
			CategoryDto selectedMainCategoryDto = null;

			if (readProduct.CategoryId.HasValue)
			{
				var parentsTree = await GetParentsTree(readProduct.CategoryId.Value);

				selectedMainCategoryDto = GetSelectedMainCategoryDto(parentsTree);

				MarkCategories(selectedMainCategoryDto.SubCategories.Concat(mainCategoriesDto), parentsTree);
			}

			return new GetFiltredViewModel
			{
				PagerOutModel = pagerOutModel,
				MainCategories = mainCategoriesDto,
				ReadProduct = readProduct,
				SelectedMainCategory = selectedMainCategoryDto
			};
		}

		private void MarkCategories(IEnumerable<CategoryDto> categories, IEnumerable<Category> parentsTree)
		{
			categories.Where(x => parentsTree.Any(y => y.Id == x.Id))
					  .ToList()
					  .ForEach(x => x.IsSelected = true);
		}

		private CategoryDto GetSelectedMainCategoryDto(IEnumerable<Category> parentsTree)
		{
			var selectedMainCategory = parentsTree.Last();
			return Mapper.Map<CategoryDto>(selectedMainCategory);
		}

		private async Task<IList<Category>> GetParentsTree(Guid categoryId)
		{
			var allCategories = await _categoryRepository.GetAllAsync();
			var targetCategory = await allCategories.FirstOrDefaultAsync(x => x.Id == categoryId);

			return FindAllParents(targetCategory, new List<Category>());
		}

		private async Task<IReadOnlyList<CategoryDto>> GetMainCategoriesAsync()
		{
			var allCategories = await _categoryRepository.GetAllAsync();
			var parentsCategories = await allCategories.Where(x => x.TopCategoryId == null).Include(c => c.SubCategories).ToListAsync();
			return Mapper.Map<IReadOnlyList<CategoryDto>>(parentsCategories);
		}

		/// <summary>
		/// Рекурсивно ищем родительские категории включая переданную.
		/// </summary>
		/// <param name="categoryId"></param>
		/// <returns></returns>
		private List<Category> FindAllParents(Category category, List<Category> result)
		{
			result.Add(category);
			if (category.TopCategoryId != null)
			{
				result.AddRange(FindAllParents(category.TopCategory, result));
			}
			return result;
		}

		public async Task AddImagesToProductAsync(CreateProduct createProduct)
		{
			createProduct.Category.Name = _categoryRepository.GetAllAsync().Result.Where(c => c.Id == createProduct.Category.Id).First()?.Name;

			var product = Mapper.Map<Product>(createProduct);
			product.Id = Guid.NewGuid();
			product.ProductCategories = new List<ProductCategory> { new ProductCategory { ProductId = product.Id, CategoryId = createProduct.Category.Id } };
			product.Images = new List<Image>();
			foreach (var image in createProduct.Images)
			{
				string path = $"content\\Images\\{createProduct.Category.Name}\\" + image.FileName;
				string savePath = _appEnvironment.WebRootPath + "\\" + path;
				// сохраняем файл в папку Files в каталоге wwwroot
				using (var fileStream = new FileStream(savePath, FileMode.Create))
				{
					await image.CopyToAsync(fileStream);
				}

				product.Images.Add(new Image
				{
					Id = Guid.NewGuid(),
					Path = path,
					ProductId = product.Id
				});
			}
			await Repository.InsertAsync(product);
		}

		public async Task<CartViewModel> GetCartViewModelAsync(CartProduct[] arr)
		{
			var productDtos = await base.ReadAsync(new ReadProduct
			{
				ProductsId = arr.Select(x => x.ProductId).ToArray()
			});
			return new CartViewModel
			{
				Products = productDtos.Select(p => new CartProductViewModel
				{
					Count = arr.First(x => x.ProductId == p.Id).Count,
					Product = p
				}).ToList()
			};
		}
	}
}
