using System;
using System.Collections.Generic;
using System.Text;
using Application.Categories.Dtos;

namespace Application.Categories
{
	public interface ICategoryService:ICrudService<Guid, CreateCategory, ReadCategory, UpdateCategory, CategoryDto>
	{

	}
}
