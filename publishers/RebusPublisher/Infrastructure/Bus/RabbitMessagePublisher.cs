using Domain.Infrastructure.Interfaces;
using Domain.Models;
using Rebus.Bus;

namespace Infrastructure.Bus
{
	public class RabbitMessagePublisher : IRabbitMessagePublisher
	{
		private readonly IBus _bus;

		public RabbitMessagePublisher(IBus bus)
		{
			_bus = bus;
		}

		public async Task PublishMessageAsync(RabbitModel message)
		{
			await _bus.Send(message);
		}
	}
}
