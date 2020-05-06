using System;
using System.IO;
using System.Threading.Tasks;
using Application.Constants;
using Application.Settings;
using Core.Entities;
using Infrastructure.EntityFramework.Context;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Host
{
	public class Program
	{
		public static async Task Main(string[] args)
		{
			var host = CreateWebHostBuilder(args).Build();

			using (var scope = host.Services.CreateScope())
			{
				try
				{
					var services = scope.ServiceProvider;

					var context = services.GetService<DbAppContext>();

					//todo
					Constant.DollarRate = 65;

					var userMng = services.GetRequiredService<UserManager<User>>();
					var roleMng = services.GetRequiredService<RoleManager<IdentityRole>>();

					await DbAppContextInitializer.InitializeAsync(roleMng,userMng);
				}
				catch (Exception ex)
				{
					var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
					logger.LogError(ex, "An error occurred while migrating or initializing the database.");
				}
			}

			await host.RunAsync();
		}

		public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
			WebHost.CreateDefaultBuilder(args)
				.ConfigureAppConfiguration((hostingContext, config) =>
					{
						var basePath = Directory.GetCurrentDirectory();
						config.SetBasePath(basePath);
						config.AddJsonFile("config.json", optional: true, reloadOnChange: true);
					})
				.UseStartup<Startup>();
	}
}
