using Rebus.Bus;
using Rebus.ServiceProvider;

namespace Domain.Infrastructure.Abstracts
{
	public abstract class MessagePublisherBase<TMessage>
	{
        protected abstract string _busName { get; }

        private readonly IBus _bus;

		protected MessagePublisherBase(IBusRegistry registry)
		{
			_bus = registry.GetBus(_busName);
		}

		public async Task PublishMessageAsync(TMessage message)
		{
			await _bus.Send(message);
		}
	}
}
