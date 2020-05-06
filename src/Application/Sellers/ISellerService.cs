using Application.Sellers.Dtos;
using System;

namespace Application.Sellers
{
	public interface ISellerService: ICrudService<Guid, CreateSeller, ReadSeller, UpdateSeller, SellerDto>
	{
	}
}
