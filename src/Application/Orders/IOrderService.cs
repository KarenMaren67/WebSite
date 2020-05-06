using Application.Orders.Dtos;
using System;

namespace Application.Orders
{
	public interface IOrderService : ICrudService<Guid, CreateOrder, ReadOrder, UpdateOrder, OrderDto>
	{
	}
}
