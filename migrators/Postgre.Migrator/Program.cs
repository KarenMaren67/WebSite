using System;
using Microsoft.Extensions.Configuration;
using Migrator;

namespace Postgre.Migrator
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine(JsonConfiguration.Configuration.GetConnectionString("PostgresqlConnectionString"));
			Console.WriteLine("Hello World!");
			Console.ReadLine();
		}
	}
}
