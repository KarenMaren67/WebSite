using System;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace Migrator
{
	public static class JsonConfiguration
	{
		private const string WebSite = "WebSite";

		private static Lazy<IConfiguration> configuration =
			new Lazy<IConfiguration>(() =>
			{
				var path = Directory.GetCurrentDirectory();
				var index = path.IndexOf(WebSite, StringComparison.Ordinal) + WebSite.Length;
				path = path.Substring(0, index);

				path = Path.GetFullPath(Path.Combine(path, "configs"));

				var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", EnvironmentVariableTarget.Process);
				return new ConfigurationBuilder()
				   .SetBasePath(path)
				   .AddJsonFile("appsettings.json")
				   .AddJsonFile($"appsettings.{environmentName}.json", true)
				   .Build(); ;
			});

		public static IConfiguration Configuration => configuration.Value;
	}
}
