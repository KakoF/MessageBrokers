using Rebus.Bus;

namespace RebusConsumer
{
	public class Worker : BackgroundService
	{
		private readonly ILogger<Worker> _logger;

		private readonly IBus _bus;

		public Worker(ILogger<Worker> logger, IBus bus)
		{
			_logger = logger;
			_bus = bus;
		}

		protected override async Task ExecuteAsync(CancellationToken stoppingToken)
		{
			// L�gica para consumir mensagens pode ser colocada aqui
			while (!stoppingToken.IsCancellationRequested)
			{
				_logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
				await Task.Delay(1000, stoppingToken);
			}
		}
	}
}
