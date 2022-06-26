namespace HelloWorld;

using System.Reflection;
using Microsoft.Azure.Functions.Worker.Extensions.OpenApi.Extensions;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;


public class Program
{
	public static void Main()
	{
		var host = CreateHost();
		host.Run();
	}

	public static IHost CreateHost()
	{
		return new HostBuilder()
			.ConfigureAppConfiguration(builder =>
			{
				builder
					.SetBasePath(Directory.GetCurrentDirectory())
					.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
					.AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", optional: true)
					.Build();
			})
			.ConfigureFunctionsWorkerDefaults(worker => worker.UseNewtonsoftJson())
			.ConfigureOpenApi()
			.ConfigureLogging((context, builder) =>
			{
				builder.AddFilter("Microsoft", LogLevel.Warning);
			})
			.ConfigureServices((builder, services) =>
			{
				var configuration = builder.Configuration;
			}).Build();
	}
}

public class OpenApiConfigurationOptions : DefaultOpenApiConfigurationOptions
{
	public OpenApiConfigurationOptions()
	{
#if !DEBUG
        ForceHttps = true;
#endif
	}

	public override OpenApiInfo Info { get; set; } = new OpenApiInfo()
	{
		Version = "1.0.0",
		Title = Assembly.GetEntryAssembly().GetName().Name,
	};
}
