using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application
{
	public interface ICrudService<TId, TCreateEntity, TReadEntity, TUpdateEntity, TEntityDto>
	{
		Task<TEntityDto> CreateAsync(TCreateEntity createEntity);
		Task DeleteAsync(TId id);
		Task<TEntityDto> ReadAsync(TId id);
		Task<IReadOnlyList<TEntityDto>> ReadAsync(TReadEntity readEntity);
		Task<TEntityDto> UpdateAsync(TUpdateEntity updateEntity);
	}
}