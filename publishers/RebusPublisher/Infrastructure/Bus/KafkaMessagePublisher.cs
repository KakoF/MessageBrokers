using Domain.Infrastructure.Abstracts;
using Domain.Infrastructure.Interfaces;
using Domain.Models;
using Rebus.Bus;
using Rebus.ServiceProvider;

namespace Infrastructure.Bus
{
	public class KafkaMessagePublisher : MessagePublisherBase<KafkaModel>, IKafkaMessagePublisher
	{
		protected override string _busName => "kafka_publisher";
		public KafkaMessagePublisher(IBusRegistry bus) : base(bus)
		{
		}
	}
}
