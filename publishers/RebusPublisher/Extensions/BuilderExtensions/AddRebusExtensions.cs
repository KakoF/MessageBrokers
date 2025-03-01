using Confluent.Kafka;
using Rebus.Auditing.Messages;
using Rebus.Config;
using Rebus.Kafka;
using Rebus.Retry.Simple;

namespace RebusPublisher.Extensions.BuilderExtensions
{
	public static class AddRebusExtensions
	{
		public static void AddServiceRebus(this IServiceCollection services, IConfigurationManager configuration)
		{

			services.AddRebus(configure => configure
						.Transport(t => t.UseKafka(
							configuration["Kafka:BrokerAddress"],
							configuration["Kafka:TopicName"],
							"RebusPublisher"))
						.Options(t => t.RetryStrategy(errorQueueName: "DLQ_" + configuration["Kafka:TopicName"]))
						.Options(t => t.EnableMessageAuditing(auditQueue: "Audit_" + configuration["Kafka:TopicName"])));
		}
	}
}
