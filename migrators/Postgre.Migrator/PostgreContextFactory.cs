using Infrastructure.EntityFramework.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Migrator;

namespace Postgre.Migrator
{
	public class PostgreContextFactory : DbAppContextFactory<DbAppContext>
	{
		protected override void Use(DbContextOptionsBuilder<DbAppContext> optionsBuilder)
		{
			var connectionString = JsonConfiguration.Configuration.GetConnectionString("PostgresqlConnectionString");
			optionsBuilder.UseNpgsql(connectionString, x => x.MigrationsAssembly("Postgre.Migrator"));
		}
	}
}
