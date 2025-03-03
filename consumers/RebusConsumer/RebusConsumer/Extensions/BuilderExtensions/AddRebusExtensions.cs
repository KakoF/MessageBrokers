using Domain.Models;
using Infraestructure.Consumers;
using Rebus.Auditing.Messages;
using Rebus.Backoff;
using Rebus.Config;
using Rebus.Kafka;
using Rebus.Retry.Simple;
using Rebus.Routing.TypeBased;

namespace RebusConsumer.Extensions.BuilderExtensions
{
	public static class AddRebusExtensions
	{
		public static void AddServiceRebus(this IServiceCollection services, IConfigurationManager configuration)
		{

			services.AddRebus(configure => configure
					.Transport(t => t.UseKafka(configuration["Kafka:BrokerAddress"], configuration["Kafka:TopicName"]))
					.Routing(r => r.TypeBased().Map<KafkaModel>(configuration["Kafka:TopicName"]))
					.Options(o => o.SetNumberOfWorkers(5))
					.Options(t => t.RetryStrategy(
						errorQueueName: "DLQ_" + configuration["Kafka:TopicName"], 
						maxDeliveryAttempts: 5 
						)
					)
					.Options(t => t.EnableMessageAuditing(auditQueue: "Audit_" + configuration["Kafka:TopicName"]))
					.Options(t => t.SetBackoffTimes(new[] {
						TimeSpan.FromSeconds(10),
						TimeSpan.FromSeconds(30),
						TimeSpan.FromSeconds(50),
						TimeSpan.FromSeconds(100),
						TimeSpan.FromSeconds(150)
					})), isDefaultBus: true, key: "kafka_publisher");

			services.AddRebus(configure => configure
					.Transport(t => t.UseRabbitMq(configuration["RabbitMq:ConnectionString"], configuration["RabbitMq:QueueName"]))
					.Routing(r => r.TypeBased().Map<RabbitModel>(configuration["RabbitMq:QueueName"]))
					.Options(o => o.SetNumberOfWorkers(5))
					.Options(t => t.RetryStrategy(
						errorQueueName: "DLQ_" + configuration["RabbitMq:QueueName"], 
						maxDeliveryAttempts: 5 
						)
					)
					.Options(t => t.EnableMessageAuditing(auditQueue: "Audit_" + configuration["RabbitMq:QueueName"]))
					.Options(t => t.SetBackoffTimes(new[] {
						TimeSpan.FromSeconds(10),
						TimeSpan.FromSeconds(30),
						TimeSpan.FromSeconds(50),
						TimeSpan.FromSeconds(100),
						TimeSpan.FromSeconds(150)
					})), isDefaultBus: false, key: "rabbit_publisher");

			services.AutoRegisterHandlersFromAssemblyOf<KafkaMessageHandler>();
			services.AutoRegisterHandlersFromAssemblyOf<RabbitMessageHandler>();

		}
	}
}
