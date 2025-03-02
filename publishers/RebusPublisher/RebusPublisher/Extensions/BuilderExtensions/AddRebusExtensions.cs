using Confluent.Kafka;
using Domain.Models;
using Rebus.Auditing.Messages;
using Rebus.Backoff;
using Rebus.Config;
using Rebus.Kafka;
using Rebus.Retry.Simple;
using Rebus.Routing.TypeBased;

namespace RebusPublisher.Extensions.BuilderExtensions
{
	public static class AddRebusExtensions
	{
		public static void AddServiceRebus(this IServiceCollection services, IConfigurationManager configuration)
		{

			/*services.AddRebus(configure => configure
						.Transport(t => t.UseKafka(
							configuration["Kafka:BrokerAddress"],
							"RebusPublisher",
							"RebusPublisher"))
						.Routing(r => r.TypeBased().Map<KafkaModel>(configuration["Kafka:TopicName"]))
						.Options(t => t.RetryStrategy(errorQueueName: "DLQ_" + configuration["Kafka:TopicName"], maxDeliveryAttempts: 5))
						.Options(t => t.EnableMessageAuditing(auditQueue: "Audit_" + configuration["Kafka:TopicName"]))
						.Options(t => t.SetBackoffTimes(new[] {
							TimeSpan.FromSeconds(1),  // 1ª tentativa (1 segundo)
							TimeSpan.FromSeconds(3),  // 2ª tentativa (3 segundos)
							TimeSpan.FromSeconds(5),  // 3ª tentativa (5 segundos)
							TimeSpan.FromSeconds(10), // 4ª tentativa (10 segundos)
							TimeSpan.FromSeconds(15)  // 5ª tentativa (15 segundos)
						})));*/
			
			services.AddRebus(configure => configure
						.Transport(t => t.UseKafkaAsOneWayClient(
							configuration["Kafka:BrokerAddress"]))
						.Routing(r => r.TypeBased().Map<KafkaModel>(configuration["Kafka:TopicName"]))
						.Options(t => t.RetryStrategy(errorQueueName: "DLQ_" + configuration["Kafka:TopicName"], maxDeliveryAttempts: 5))
						.Options(t => t.EnableMessageAuditing(auditQueue: "Audit_" + configuration["Kafka:TopicName"]))
						.Options(t => t.SetBackoffTimes(new[] {
							TimeSpan.FromSeconds(1),  // 1ª tentativa (1 segundo)
							TimeSpan.FromSeconds(3),  // 2ª tentativa (3 segundos)
							TimeSpan.FromSeconds(5),  // 3ª tentativa (5 segundos)
							TimeSpan.FromSeconds(10), // 4ª tentativa (10 segundos)
							TimeSpan.FromSeconds(15)  // 5ª tentativa (15 segundos)
						})));
		}
	}
}
