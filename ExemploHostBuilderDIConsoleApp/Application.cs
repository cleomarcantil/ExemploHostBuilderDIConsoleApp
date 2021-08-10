using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ExemploHostBuilderDIConsoleApp
{
	class Application : IHostedService
	{
		private readonly ILogger<Application> logger;
		private readonly TesteService testeService;

		public Application(ILogger<Application> logger, TesteService testeService)
		{
			this.logger = logger;
			this.testeService = testeService;
		}

		public async Task StartAsync(CancellationToken cancellationToken)
		{
			Console.WriteLine("Aplicação iniciada");

			Console.WriteLine("Executando...");

			var checkCancellationTask = Task.Run(async () =>
			{
				while (!cancellationToken.IsCancellationRequested)
					await Task.Delay(100, cancellationToken);
			});

			var inputTask = Task.Run(() =>
			{
				while (!cancellationToken.IsCancellationRequested)
				{
					Console.Write("Digite algo: ");
					
					if (Console.ReadLine() is { } input)
						Console.WriteLine($"TesteService: {testeService.FormatarMensagem(input)}");
					
					Console.WriteLine();
				}
			}, cancellationToken);

			await Task.WhenAny(checkCancellationTask, inputTask);
		}

		public async Task StopAsync(CancellationToken cancellationToken)
		{
			Console.WriteLine("Aplicação encerrada");

		}
	}
}
