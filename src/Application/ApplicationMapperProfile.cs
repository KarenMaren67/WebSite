using Application.Categories.Dtos;
using Application.Images.Dtos;
using Application.Orders.Dtos;
using Application.Products.Dtos;
using Application.Sellers.Dtos;
using Application.Users.Dtos;
using AutoMapper;
using Core;
using Core.Entities;

namespace Application
{
	public class ApplicationMapperProfile: Profile 
	{
		public ApplicationMapperProfile()
		{
			CreateMap<Product, ProductDto>();
			CreateMap<CreateProduct, Product>().ForMember(e => e.Images, x => x.Ignore());
			CreateMap<ProductDto, UpdateProduct>();
			CreateMap<UpdateProduct, Product>();
			CreateMap<Image, ImageDto>();
			CreateMap<Seller, SellerDto>();
			CreateMap<Order, OrderDto>();
			CreateMap<Category, CategoryDto>();
			CreateMap<CreateCategory, Category>();
			CreateMap<CategoryDto, UpdateCategory>();
			CreateMap<UpdateCategory, Category>();

			CreateMap<CreateOrder, Order>();
			CreateMap<User, UserDto>();
		}
	}
}
