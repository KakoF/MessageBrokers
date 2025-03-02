using Domain.Infrastructure.Abstracts;
using Domain.Infrastructure.Interfaces;
using Domain.Models;
using Rebus.ServiceProvider;

namespace Infrastructure.Bus
{
	public class RabbitMessagePublisher : MessagePublisherBase<RabbitModel>, IRabbitMessagePublisher
	{
		protected override string _busName => "rabbit_publisher";
		public RabbitMessagePublisher(IBusRegistry bus) : base(bus)
		{
		}
	}
}
