using Domain.Infrastructure.Interfaces;
using Infrastructure.Bus;

namespace RebusPublisher.Extensions.BuilderExtensions
{
    public static class DomainExtensions
	{
        public static void AddDomainExtensions(this IServiceCollection services, IConfigurationManager configuration)
		{
			services.AddScoped<IKafkaMessagePublisher, KafkaMessagePublisher>();
			services.AddScoped<IRabbitMessagePublisher, RabbitMessagePublisher>();
			//services.AddSingleton(new KafkaInitializer(configuration["Kafka:BrokerAddress"]!));
		}
    }
}
