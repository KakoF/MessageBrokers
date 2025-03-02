using Domain.Infrastructure.Interfaces;
using Domain.Models;
using Rebus.Bus;

namespace Infrastructure.Bus
{
	public class KafkaMessagePublisher : IKafkaMessagePublisher
	{
		private readonly IBus _bus;

		public KafkaMessagePublisher(IBus bus)
		{
			_bus = bus;
		}

		public async Task PublishMessageAsync(KafkaModel message)
		{
			await _bus.Send(message);
		}
	}
}
