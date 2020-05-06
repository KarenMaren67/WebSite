using Application.Sellers.Dtos;
using AutoMapper;
using Core;
using Infrastructure;
using System;
using Core.Entities;

namespace Application.Sellers
{
	internal class SellerService : CrudService<Seller, Guid, CreateSeller, ReadSeller, UpdateSeller, SellerDto>, ISellerService
	{
		public SellerService(IRepository<Seller, Guid> repository, IMapper mapper) : 
			base(repository, mapper)
		{
		}

	}
}
