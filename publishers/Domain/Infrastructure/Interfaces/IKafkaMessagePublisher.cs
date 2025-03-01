
namespace Domain.Infrastructure.Interfaces
{
	public interface IKafkaMessagePublisher
	{
		public Task PublishMessageAsync(object message);
	}
}
