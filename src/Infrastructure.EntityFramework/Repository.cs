using System;
using System.Linq;
using System.Threading.Tasks;
using Core;
using Core.Infrastructure;
using Infrastructure.EntityFramework.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EntityFramework
{
	public class Repository<TEntity,TId> : IRepository<TEntity,TId>
		where TEntity :class, IEntity<TId> 
		where TId:IEquatable<TId>
	{
		private readonly DbContext _context;
		private DbSet<TEntity> Table => _context.Set<TEntity>();

		public Repository(DbAppContext context)
		{
			_context = context;
		}

		public async Task DeleteAsync(TId id)
		{
			Table.Remove( await Table.FirstAsync(x => x.Id.Equals(id)));
		}

		public Task<IQueryable<TEntity>> GetAllAsync()
		{
			return Task.FromResult<IQueryable<TEntity>>(Table);
		}

		public Task<TEntity> InsertAsync(TEntity entity)
		{
			Table.Add(entity);
			return Task.FromResult<TEntity>(entity);
		}

		public Task<TEntity> UpdateAsync(TEntity entity)
		{
			return Task.FromResult(Table.Update(entity).Entity) ;
		}
	}
}
