using Application.Categories;
using Application.Images;
using Application.Orders;
using Application.Products;
using Application.Sellers;
using Microsoft.Extensions.DependencyInjection;

namespace Application
{
	public static class ApplicationModule
	{
		public static IServiceCollection ApplicationConfigure(this IServiceCollection serviceCollection)
		{
			serviceCollection.AddScoped<IProductService,ProductService>();
			serviceCollection.AddScoped<IOrderService, OrderService>();
			serviceCollection.AddScoped<ISellerService, SellerService>();
			serviceCollection.AddScoped<IImageService, ImageService>();
			serviceCollection.AddScoped<ICategoryService, CategoryService>();


			return serviceCollection;
		}
	}
}
