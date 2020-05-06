using Application.Images.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Images
{
	public interface IImageService: ICrudService<Guid, CreateImage, ReadImage, UpdateImage, ImageDto>
	{
	}
}
