using Core.Entities;
using Infrastructure.EntityFramework.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Mysql.Migrator.Context
{
	public class MySqlContext : DbAppContext
	{
		public MySqlContext(DbContextOptions<MySqlContext> options) : base(options)
		{
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			// Shorten key length for Identity
			modelBuilder.Entity<User>(entity =>
			{
				entity.Property(m => m.Id).HasMaxLength(127);
				entity.Property(m => m.PhoneNumber).HasMaxLength(127);
				entity.Property(m => m.ConcurrencyStamp).HasMaxLength(127);
				entity.Property(m => m.SecurityStamp).HasMaxLength(127);
				entity.Property(m => m.PasswordHash).HasMaxLength(127);
				entity.Property(m => m.NormalizedEmail).HasMaxLength(127);
				entity.Property(m => m.Email).HasMaxLength(127);
				entity.Property(m => m.NormalizedUserName).HasMaxLength(127);
				entity.Property(m => m.UserName).HasMaxLength(127);
			});
			modelBuilder.Entity<IdentityRole>(entity =>
			{
				entity.Property(m => m.Id).HasMaxLength(127);
				entity.Property(m => m.Name).HasMaxLength(127);
				entity.Property(m => m.NormalizedName).HasMaxLength(127);
				entity.Property(m => m.ConcurrencyStamp).HasMaxLength(127);
			});
			modelBuilder.Entity<IdentityUserLogin<string>>(entity =>
			{
				entity.Property(m => m.LoginProvider).HasMaxLength(127);
				entity.Property(m => m.ProviderKey).HasMaxLength(127);
			});
			modelBuilder.Entity<IdentityUserRole<string>>(entity =>
			{
				entity.Property(m => m.UserId).HasMaxLength(127);
				entity.Property(m => m.RoleId).HasMaxLength(127);
			});
			modelBuilder.Entity<IdentityUserToken<string>>(entity =>
			{
				entity.HasKey(x => x.UserId);

				entity.Property(m => m.UserId).HasMaxLength(127);
				entity.Property(m => m.LoginProvider).HasMaxLength(127);
				entity.Property(m => m.Name).HasMaxLength(127);
				entity.Property(m => m.Value).HasMaxLength(127);

				//entity.HasOne<User>().WithMany().HasForeignKey("FKUserTokensUsers");
			});

		}
	}
}
