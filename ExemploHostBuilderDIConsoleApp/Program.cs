using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading.Tasks;

namespace ExemploHostBuilderDIConsoleApp
{
	class Program
	{
		static async Task Main(string[] args)
		{
			Console.WriteLine("ExemploHostBuilderDIConsoleApp");

			await CreateHostBuilder(args)
				.RunConsoleAsync();
		}

		public static IHostBuilder CreateHostBuilder(string[] args) =>
			Host.CreateDefaultBuilder(args)
				.ConfigureServices((context, services) =>
				{
					services.AddHostedService<Application>();

					new Startup(context.Configuration)
						.ConfigureServices(services);
				});

	}
}
