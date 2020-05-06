using System.Threading.Tasks;
using Infrastructure.EntityFramework.Context;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.EntityFramework.Middlewsres
{
	public class UowMiddleware
	{
		private readonly RequestDelegate _next;

		public UowMiddleware(RequestDelegate next)
		{
			_next = next;
		}

		public async Task Invoke(HttpContext httpContext, DbAppContext db)
		{
			await _next.Invoke(httpContext);

			if (db.ChangeTracker.HasChanges())
			{
				 await db.SaveChangesAsync();
			}
		}
	}
}
