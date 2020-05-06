using Application.Images.Dtos;
using AutoMapper;
using Core;
using Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;
using Core.Entities;

namespace Application.Images
{
	internal class ImageService : CrudService<Image, Guid, CreateImage, ReadImage, UpdateImage, ImageDto>, IImageService
	{
		public ImageService(IRepository<Image, Guid> repository, IMapper mapper) : 
			base(repository, mapper)
		{
		}
	}
}
