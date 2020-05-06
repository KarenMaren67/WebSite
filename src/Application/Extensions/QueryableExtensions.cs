using System;
using System.Linq;
using System.Linq.Expressions;

namespace Application.Extensions
{
	public static class QueryableExtensions
	{
		public static IQueryable<T> GetPaged<T>(this IQueryable<T> items, PagerInput input)
		{
			return items.Skip(input.CountItemPerPage * (input.CurrentPage - 1)).Take(input.CountItemPerPage);
		}

		public static IQueryable<TSource> WhereIf<TSource>(this IQueryable<TSource> source, bool condition, Expression<Func<TSource, bool>> predicate)
		{
			if (!condition)
			{
				return source;
			}

			return source.Where(predicate);
		}
 
		public static IQueryable<TSource> WhereIf<TSource>(this IQueryable<TSource> source, bool condition, Expression<Func<TSource, int, bool>> predicate)
		{
			if (!condition)
			{
				return source;
			}

			return source.Where(predicate);
		}
	}
}
