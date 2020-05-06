namespace Core.Infrastructure
{
	public interface IEntity<TId>
	{
		TId Id { get; set; }
	}
}
