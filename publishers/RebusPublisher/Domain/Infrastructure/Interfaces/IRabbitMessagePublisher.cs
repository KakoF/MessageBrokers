
using Domain.Models;

namespace Domain.Infrastructure.Interfaces
{
	public interface IRabbitMessagePublisher : IMessagePublisher<RabbitModel>
	{
		//public Task PublishMessageAsync(RabbitModel message);
	}
}
