using Application;
using Application.Infrastructure;
using AutoMapper;
using Core.Entities;
using Infrastructure;
using Infrastructure.EntityFramework;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Infrastructure.EntityFramework.Context;
using Infrastructure.EntityFramework.Middlewsres;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Host
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		public void ConfigureServices(IServiceCollection services)
		{
			StartupConfigureServices(services);

			services.AddTransient<IEmailSender,EmailSender>();
		}

		public void ConfigureDevelopmentServices(IServiceCollection services)
		{
			StartupConfigureServices(services);

			//todo для отладки отправки почты
			//services.AddTransient<IEmailSender,EmailSender>();
		}

		public void ConfigurePrereleaseServices(IServiceCollection services)
		{
			StartupConfigureServices(services);

			services.AddTransient<IEmailSender,EmailSender>();
		}

		// This method gets called by the runtime. Use this method to add services to the container.
		private void StartupConfigureServices(IServiceCollection services)
		{
			services.AddEntityFrameworkNpgsql()
				 .AddDbContext<DbAppContext>(options =>
												 options.UseNpgsql(
													 Configuration.GetConnectionString("PostgresqlConnectionString")));

			services.Configure<CookiePolicyOptions>(options =>
			{
				// This lambda determines whether user consent for non-essential cookies is needed for a given request.
				options.CheckConsentNeeded = context => true;
				options.MinimumSameSitePolicy = SameSiteMode.None;
			});

			services.AddDefaultIdentity<User>(config =>
					 {
						 config.Password.RequireDigit = false;
						 config.Password.RequireLowercase = false;
						 config.Password.RequireUppercase = false;
						 config.Password.RequireNonAlphanumeric = false;
						 config.Password.RequiredLength = 6;
					 })
					 .AddRoles<IdentityRole>()
					 .AddClaimsPrincipalFactory<UserClaimsPrincipalFactory<User, IdentityRole>>()
					 .AddEntityFrameworkStores<DbAppContext>();

			services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

			services.ApplicationConfigure();

			services.AddScoped(typeof(IRepository<,>), typeof(Repository<,>));

			services.AddAutoMapper();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			if (env.IsDevelopment() || env.IsEnvironment("Prerelease"))
			{
				app.UseDeveloperExceptionPage();
				app.UseDatabaseErrorPage();
			}
			else
			{
				app.UseExceptionHandler("/Home/Error");
				app.UseHsts();
			}

			app.UseHttpsRedirection();
			app.UseStaticFiles();
			app.UseCookiePolicy();

			app.UseAuthentication();

			app.UseMiddleware<UowMiddleware>();

			//app.UseMvcWithDefaultRoute();
			app.UseMvc(routes =>
			{
				routes.MapRoute(
					name: "areas",
					template: "{area:exists}/{controller=Home}/{action=Index}");
				routes.MapRoute(
					name: "default",
					template: "{controller=Home}/{action=Index}/{id?}");
			});
		}
	}
}
