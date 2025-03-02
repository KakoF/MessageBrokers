
using Domain.Models;

namespace Domain.Infrastructure.Interfaces
{
	public interface IKafkaMessagePublisher
	{
		public Task PublishMessageAsync(KafkaModel message);
	}
}
