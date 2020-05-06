using System;
using Application.Categories;
using Application.Categories.Dtos;
using AutoFixture;
using AutoMapper;
using Core.Entities;
using FluentAssertions;
using Infrastructure;
using Moq;
using Xunit;

namespace Application.Tests
{
	public class CategoryServiceTests
	{
		private ICategoryService _categoryService;
		private Mock<IRepository<Category, Guid>> _categoryRepositoryMock;
		private Mock<IMapper> _mapper;
		private IFixture _fixture;

		public CategoryServiceTests()
		{
			_categoryRepositoryMock = new Mock<IRepository<Category, Guid>>(MockBehavior.Strict);
			//var mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile<ApplicationMapperProfile>()));

			_mapper = new Mock<IMapper>(MockBehavior.Strict);

			_fixture = new Fixture();
			_fixture.Behaviors.Add(new OmitOnRecursionBehavior());

			_categoryService = new CategoryService(_categoryRepositoryMock.Object,_mapper.Object);
		}

		[Fact]
		public void CreateTest()
		{
			var createEntity = _fixture.Create<CreateCategory>();
			var category = _fixture.Create<Category>();
			var categoryDto = _fixture.Create<CategoryDto>();

			_mapper.Setup(x => x.Map<Category>(createEntity)).Returns(category);
			_categoryRepositoryMock.Setup(x => x.InsertAsync(category)).ReturnsAsync(category);
			_mapper.Setup(x => x.Map<CategoryDto>(category)).Returns(categoryDto);

			var result = _categoryService.CreateAsync(createEntity).Result;

			result.Should().Be(categoryDto);
		}
	}
}
