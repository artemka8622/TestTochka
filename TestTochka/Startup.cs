using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using TestTochka.Service;

using VkNet;
using VkNet.Abstractions;

namespace TestTochka
{
	/// <summary>
	/// Класс запуска приложения.
	/// </summary>
	public class Startup
	{
		/// <summary>
		/// Инициализация приложения.
		/// </summary>
		/// <returns>Разрешение зависимостей.</returns>
		public IServiceProvider Init()
		{
			//setup our DI
			var serviceProvider = new ServiceCollection()
				.AddLogging(configure => configure.AddConsole())
				.AddScoped<IExternalApiService, VkPostService>()
				.AddScoped<IStatisticService, StatisticService>()
				.AddScoped<IVkApi, VkApi>(t => new VkApi())
				.AddSingleton(new LoggerFactory()
						.AddConsole())
				.BuildServiceProvider();

			//configure console logging
			serviceProvider.GetService<ILoggerFactory>().AddConsole(LogLevel.Debug);

			var logger = serviceProvider.GetService<ILoggerFactory>()
				.CreateLogger<Program>();
			logger.LogDebug("Starting application");

			logger.LogDebug("All done!");
			return serviceProvider;
		}

		/// <summary>
		/// Запускает приложение.
		/// </summary>
		/// <param name="service_provider">The service Provider.</param>
		public void Start(IServiceProvider service_provider)
		{
			var external_service = service_provider.GetService<IExternalApiService>();
			var statistic_service = service_provider.GetService<IStatisticService>();
			external_service.Authorize("test", "test");
			while (true)
			{
				Console.WriteLine("Enter input:"); // Prompt
				string user_id = Console.ReadLine(); // Get string from user
				if (user_id == "exit") // Check string
				{
					break;
				}

				var posts = external_service.GetPosts(user_id, 5);
				var statistic = statistic_service.GetStatistic(posts);
				external_service.SendPosts(user_id, statistic);
			}

		}

	}
}
