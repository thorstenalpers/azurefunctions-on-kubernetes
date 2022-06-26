namespace HelloWorld;

using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

public class TimerTrigger
{
	private readonly ILogger<TimerTrigger> _logger;

	public TimerTrigger(ILogger<TimerTrigger> logger)
	{
		_logger = logger;
	}


	[Singleton]
	[Function(nameof(LogHello))]
	public void LogHello([TimerTrigger("0 20 10 * * *", UseMonitor = false, RunOnStartup = true)] TimerInfo timerInfo)
	{
		// täglich um 20:10 Uhr
		_logger.LogInformation("Hello World");
	}
}
