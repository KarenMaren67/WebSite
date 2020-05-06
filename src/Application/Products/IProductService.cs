using System;
using System.Threading.Tasks;
using Application.Products.Dtos;
using Application.Products.Dtos.Cart;
using Application.Products.Dtos.ViewModels;

namespace Application.Products
{
	public interface IProductService: ICrudService<Guid, CreateProduct, ReadProduct, UpdateProduct, ProductDto>
	{
		Task<GetFiltredViewModel> GetFiltredAsync(ReadProduct readProduct);

		Task<ProductListForAdminViewModel> GetProductListForAdminViewModelAsync(ReadProduct readProduct);

		Task AddImagesToProductAsync(CreateProduct product);
		Task<CartViewModel> GetCartViewModelAsync(CartProduct[] arr);
	}
}
