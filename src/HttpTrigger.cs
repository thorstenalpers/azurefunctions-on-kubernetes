namespace HelloWorld;

using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

public class HttpTrigger
{
	private readonly ILogger<HttpTrigger> _logger;

	public HttpTrigger(ILogger<HttpTrigger> logger)
	{
		_logger = logger;
	}

	[Function(nameof(SayHello))]
	[OpenApiOperation(operationId: nameof(SayHello))]
	public string SayHello([HttpTrigger(AuthorizationLevel.Anonymous, "get")] HttpRequestData request)
	{
		return "Hello World";
	}
}
