using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure
{
	public interface IRepository<TEntity, in TId>
	{
		Task<IQueryable<TEntity>> GetAllAsync();

		Task<TEntity> InsertAsync(TEntity entity);

		Task DeleteAsync(TId id);

		Task<TEntity> UpdateAsync(TEntity entity);
	}
}
