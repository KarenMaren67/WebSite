using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Migrator
{
	public abstract class DbAppContextFactory<TContext> : IDesignTimeDbContextFactory<TContext> where TContext : DbContext
	{
		public TContext CreateDbContext(string[] args)
		{
			var optionsBuilder = new DbContextOptionsBuilder<TContext>();

			Use(optionsBuilder);

			var options = optionsBuilder.Options;

			return (TContext)Activator.CreateInstance(typeof(TContext), options); 
		}

		protected abstract void Use(DbContextOptionsBuilder<TContext> optionsBuilder);

	}
}
