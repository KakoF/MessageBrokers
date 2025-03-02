using Domain.Infrastructure.Interfaces;
using Domain.Models;
using Rebus.Bus;
using Rebus.ServiceProvider;

namespace Infrastructure.Bus
{
	public class RabbitMessagePublisher : IRabbitMessagePublisher
	{
		private readonly IBus _bus;

		public RabbitMessagePublisher(IBusRegistry registry)
		{
			_bus = registry.GetBus("rabbit_publisher");
		}

		public async Task PublishMessageAsync(RabbitModel message)
		{
			await _bus.Send(message);
		}
	}
}
