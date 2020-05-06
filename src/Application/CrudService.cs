using System;
using AutoMapper;
using Infrastructure;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Core.Infrastructure;

namespace Application
{
	public abstract class CrudService<TEntity, TId, TCreateEntity, TReadEntity, TUpdateEntity, TEntityDto> :
		ICrudService<TId, TCreateEntity, TReadEntity, TUpdateEntity, TEntityDto>
		where TEntity : IEntity<TId>
		where TId : IEquatable<TId>
		where TReadEntity: new()
	{
		protected readonly IRepository<TEntity, TId> Repository;
		protected readonly IMapper Mapper;

		protected CrudService(IRepository<TEntity, TId> repository, IMapper mapper)
		{
			Repository = repository;
			Mapper = mapper;
		}

		public virtual async Task<TEntityDto> CreateAsync(TCreateEntity createEntity)
		{
			var entity = Mapper.Map<TEntity>(createEntity);

			entity = await Repository.InsertAsync(entity);
			return Mapper.Map<TEntityDto>(entity);
		}

		public virtual async Task<TEntityDto> ReadAsync(TId id)
		{
			var entities = Read(await Repository.GetAllAsync(), new TReadEntity());
			return Mapper.Map<TEntityDto>(await entities.FirstAsync(x=>x.Id.Equals(id)));
		}

		public virtual async Task<TEntityDto> UpdateAsync(TUpdateEntity updateEntity)
		{
			dynamic updateEntityDynamic = updateEntity;
			IEquatable<TId> id = updateEntityDynamic.Id;

			var entity = await (await Repository.GetAllAsync()).FirstAsync(x => x.Id.Equals(id));

			Mapper.Map(updateEntity, entity);

			entity = await Repository.UpdateAsync(entity);
			return Mapper.Map<TEntityDto>(entity);
		}

		public virtual Task DeleteAsync(TId id)
		{
			return Repository.DeleteAsync(id);
		}

		public virtual async Task<IReadOnlyList<TEntityDto>> ReadAsync(TReadEntity readEntity)
		{
			var entities = await Repository.GetAllAsync();
			return Mapper.Map<IReadOnlyList<TEntityDto>>(await Read(entities,readEntity).ToListAsync());
		}

		protected virtual IQueryable<TEntity> Read(IQueryable<TEntity> entities, TReadEntity readEntity)
		{
			return entities;
		}
	}
}
