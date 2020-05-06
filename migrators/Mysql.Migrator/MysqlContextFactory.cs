using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Migrator;
using Mysql.Migrator.Context;

namespace Mysql.Migrator
{
	public class MysqlContextFactory : DbAppContextFactory<MySqlContext>
	{
		protected override void Use(DbContextOptionsBuilder<MySqlContext> optionsBuilder)
		{
			var connectionString = JsonConfiguration.Configuration.GetConnectionString("MysqlConnectionString");
			
			//optionsBuilder.UseMySql(connectionString, x => x.MigrationsAssembly("Mysql.Migrator"));//pomelo.entityframeworkcore.mysql
			optionsBuilder.UseMySQL(connectionString, x => x.MigrationsAssembly("Mysql.Migrator"));//mysql.data.entityframeworkcore
		}
	}
}
