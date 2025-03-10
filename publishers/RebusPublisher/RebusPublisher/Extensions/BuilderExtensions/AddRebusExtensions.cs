﻿using Domain.Models;
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
			
			services.AddRebus(configure => configure
						.Transport(t => t.UseKafkaAsOneWayClient(configuration["Kafka:BrokerAddress"]))
						.Routing(r => r.TypeBased().Map<KafkaModel>(configuration["Kafka:TopicName"]))
						.Options(t => t.RetryStrategy(errorQueueName: "DLQ_" + configuration["Kafka:TopicName"], maxDeliveryAttempts: 5))
						.Options(t => t.EnableMessageAuditing(auditQueue: "Audit_" + configuration["Kafka:TopicName"]))
						/*.Options(t => t.SetBackoffTimes(new[] {
							TimeSpan.FromSeconds(1),
							TimeSpan.FromSeconds(3),
							TimeSpan.FromSeconds(5),
							TimeSpan.FromSeconds(10),
							TimeSpan.FromSeconds(15)
						}))*/, isDefaultBus: true, key: "kafka_publisher");



			services.AddRebus(configure => configure
						.Transport(t => t.UseRabbitMqAsOneWayClient(connectionString: configuration["RabbitMq:ConnectionString"]))
						.Routing(r => r.TypeBased().Map<RabbitModel>(configuration["RabbitMq:QueueName"]))
						.Options(t => t.RetryStrategy(errorQueueName: "DLQ_" + configuration["RabbitMq:QueueName"], maxDeliveryAttempts: 5))
						.Options(t => t.EnableMessageAuditing(auditQueue: "Audit_" + configuration["RabbitMq:QueueName"]))
						/*.Options(t => t.SetBackoffTimes(new[] {
							TimeSpan.FromSeconds(1),
							TimeSpan.FromSeconds(3),
							TimeSpan.FromSeconds(5),
							TimeSpan.FromSeconds(10),
							TimeSpan.FromSeconds(15)
						}))*/, isDefaultBus: false, key: "rabbit_publisher");

		}
	}
}
