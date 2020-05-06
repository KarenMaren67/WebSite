using Application.Categories.Dtos;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Infrastructure;

namespace Application.Categories
{
	internal class CategoryService:  CrudService<Category, Guid, CreateCategory, ReadCategory, UpdateCategory, CategoryDto>, ICategoryService
	{
		public CategoryService(IRepository<Category, Guid> repository, IMapper mapper) : base(repository, mapper)
		{
		}
	}
}
