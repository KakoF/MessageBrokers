using Domain.Infrastructure.Interfaces;
using Domain.Models;
using Microsoft.Extensions.Configuration;
using Rebus.Bus;

namespace Infrastructure.Bus
{
	public class KafkaMessagePublisher : IKafkaMessagePublisher
	{
		private readonly IBus _bus;
		//private readonly string _topicName;

		public KafkaMessagePublisher(IBus bus)//, IConfiguration configuration)
		{
			_bus = bus;
			//_topicName = configuration["Kafka:TopicName"]!;
		}

		public async Task PublishMessageAsync(KafkaModel message)
		{
			//await _bus.Advanced.Topics.Send(_topicName, message, key);
			await _bus.Send(message);

			//await _bus.Advanced.Topics.Publish(_topicName, message);
		}
	}
}
