using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Core.Entities;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.EntityFramework.Context
{
	public class DbAppContextInitializer
	{

		public static async Task InitializeAsync(RoleManager<IdentityRole> roleManager, UserManager<User> userManager)
		{
			if (!await roleManager.RoleExistsAsync("Admin"))
			{
				await roleManager.CreateAsync(new IdentityRole("Admin"));
			}

			var users = new[]
			{
				new User{UserName = "davtian555@yandex.ru",Email = "davtian555@yandex.ru"}
			};

			foreach (var user in users)
			{
				if (await userManager.FindByEmailAsync(user.Email) == null)
				{
					var result = await userManager.CreateAsync(user, "1david1");

					if (result.Succeeded)
					{
						await userManager.AddToRoleAsync(user, "Admin");
					}
				}
			}

		}

	}
}
