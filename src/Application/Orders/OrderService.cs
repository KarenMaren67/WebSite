using Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Infrastructure;
using Application.Orders.Dtos;
using Application.Products.Dtos;
using Core.Entities;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Application.Products.Dtos.Cart;

namespace Application.Orders
{
	internal class OrderService : CrudService<Order, Guid, CreateOrder, ReadOrder, UpdateOrder, OrderDto>, IOrderService
	{
		private readonly IEmailSender _emailSender;
		private readonly IRepository<Product, Guid> _productRepository;

		public OrderService(IRepository<Order, Guid> repository,
							IMapper mapper,
							IEmailSender emailSender,
							IRepository<Product, Guid> productRepository) :
			base(repository, mapper)
		{
			_emailSender = emailSender;
			_productRepository = productRepository;
		}

		public override async Task<OrderDto> CreateAsync(CreateOrder model)
		{
			var order = new Order
			{
				Id = Guid.NewGuid(),
				Customer = new User { PhoneNumber = model.Phone, UserName = model.Name },
				Seller = new Seller
				{
					Email = "cerama-shop@yandex.ru",
					Name = "Рафаэль"
				}
			};

			var productOrders = model.CartProducts.Select(x => new ProductOrder
			{
				OrderId = order.Id,
				ProductId = x.ProductId,
				ProductCount = x.Count
			}).ToList();

			productOrders.ForEach(x => order.OrderProducts.Add(x));

			await Repository.InsertAsync(order);

			var allProducts = await _productRepository.GetAllAsync();
			var products = await allProducts.Where(x => model.CartProducts.Any(y => y.ProductId == x.Id)).ToListAsync();
			var productDto = Mapper.Map<IReadOnlyList<ProductDto>>(products);

			var productsViewModels = productDto.Select(p => new CartProductViewModel
			{
				Count = model.CartProducts.First(x => x.ProductId == p.Id).Count,
				Product = p
			}).ToList();

			await _emailSender.SendEmailAsync("cerama-shop@yandex.ru", "Новый заказ", $@"Пользователь {model.Name} оставил заказ на товары: {string.Join(", ",productsViewModels.Select(x=>$"Артикул: {x.Product.Articul}, Количество: {x.Count}, Стоимость: {x.Price}"))}.
Номер телефона для связи {model.Phone}.
Сумма заказа {model.Price}.");

			return Mapper.Map<OrderDto>(order);
		}
	}
}
