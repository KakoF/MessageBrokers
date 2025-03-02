
using Domain.Models;

namespace Domain.Infrastructure.Interfaces
{
	public interface IKafkaMessagePublisher: IMessagePublisher<KafkaModel>
	{
		//public Task PublishMessageAsync(KafkaModel message);
	}
}
