using System;
using Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EntityFramework.Context
{
	public class DbAppContext :IdentityDbContext<User, IdentityRole, string, IdentityUserClaim<string>, IdentityUserRole<string>, IdentityUserLogin<string>, IdentityRoleClaim<string>, IdentityUserToken<string>>

	{

		//public DbAppContext(DbContextOptions<DbAppContext> options)
		//	: base(options)
		//{
		//}

		public DbAppContext(DbContextOptions options)
			: base(options)
		{
		}

		public DbSet<Product> Products { get; set; }
		public DbSet<Order> Orders { get; set; }
		public DbSet<Seller> Sellers { get; set; }
		public DbSet<Image> Images { get; set; }
		public DbSet<Setting> Settings { get; set; }
		public DbSet<Category> Categories { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			ProductOrderModelCreating(modelBuilder);
			ProductCategoryModelCreating(modelBuilder);
			OrderModelCreating(modelBuilder);
			CategoryModelCreating(modelBuilder);
		}

		private void CategoryModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Category>()
						.HasOne(x => x.TopCategory)
						.WithMany(x => x.SubCategories)
						.HasForeignKey(x => x.TopCategoryId)
						.IsRequired(false);
		}

		private void ProductCategoryModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<ProductCategory>()
					 .HasKey(t => new {t.ProductId, t.CategoryId});

			modelBuilder.Entity<ProductCategory>()
					 .HasOne(pt => pt.Product)
					 .WithMany(p => p.ProductCategories)
					 .HasForeignKey(pt => pt.ProductId);

			modelBuilder.Entity<ProductCategory>()
					 .HasOne(pt => pt.Category)
					 .WithMany(t => t.ProductCategories)
					 .HasForeignKey(pt => pt.CategoryId);
		}

		private void ProductOrderModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<ProductOrder>()
					 .HasKey(t => new {t.ProductId, t.OrderId});

			modelBuilder.Entity<ProductOrder>()
					 .HasOne(pt => pt.Product)
					 .WithMany(p => p.ProductOrders)
					 .HasForeignKey(pt => pt.ProductId);

			modelBuilder.Entity<ProductOrder>()
					 .HasOne(pt => pt.Order)
					 .WithMany(t => t.OrderProducts)
					 .HasForeignKey(pt => pt.OrderId);
		}

		private void OrderModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Order>().HasOne(o => o.Customer)
					 .WithMany(x => x.Orders)
					 .HasForeignKey(x => x.CustomerId);
		}
	}
}
