using Confluent.Kafka;
using Confluent.Kafka.Admin;

public class KafkaInitializer
{
	private readonly AdminClientConfig _adminConfig;

	public KafkaInitializer(string brokerAddress)
	{
		_adminConfig = new AdminClientConfig { BootstrapServers = brokerAddress };
	}

	public async Task EnsureTopicExistsAsync(string topicName)
	{
		using (var adminClient = new AdminClientBuilder(_adminConfig).Build())
		{
			try
			{
				var metadata = adminClient.GetMetadata(TimeSpan.FromSeconds(10));
				var topicExists = metadata.Topics.Any(t => t.Topic == topicName);

				if (!topicExists)
				{
					await adminClient.CreateTopicsAsync(new List<TopicSpecification>
					{
						new TopicSpecification
						{
							Name = topicName,
							NumPartitions = 1,
							ReplicationFactor = 1
						}
					});
					Console.WriteLine($"Tópico '{topicName}' criado.");
				}
				else
				{
					Console.WriteLine($"Tópico '{topicName}' já existe.");
				}
			}
			catch (CreateTopicsException ex)
			{
				// Handle specific topic creation exceptions
				Console.WriteLine($"Erro ao criar tópico: {ex.Results[0].Error.Reason}");
			}
			catch (Exception ex)
			{
				// Handle other exceptions
				Console.WriteLine($"Erro ao verificar/criar o tópico: {ex.Message}");
			}
		}
	}
}
