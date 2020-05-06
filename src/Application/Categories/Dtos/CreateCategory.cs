using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Application.Categories.Dtos
{
	public class CreateCategory
	{
		[Display(Name = "Название категории")]
		[Required]
		public string Name { get; set; }

		public Guid TopCategoryId { get; set; }
	}
}
