using System;
using System.ComponentModel.DataAnnotations;

namespace Application.Categories.Dtos
{
	public class UpdateCategory
	{
		public Guid Id { get; set; }

		[Display(Name = "Название категории")]
		[Required]
		public string Name { get; set; }

		public Guid TopCategoryId { get; set; }
	}
}
