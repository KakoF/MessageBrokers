namespace Domain.Infrastructure.Interfaces
{
	public interface IMessagePublisher<T> where T : class
	{
		public Task PublishMessageAsync(T message);
	}
}
