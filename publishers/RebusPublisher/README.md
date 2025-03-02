# Rebus

Rebus é uma biblioteca de mensageria para .NET que facilita a integração com sistemas de filas, brokers de mensagens e sistemas distribuídos. Ele oferece uma maneira simples e eficaz de enviar e receber mensagens em sistemas assíncronos, além de abstrair detalhes como gerenciamento de filas, retries, e auditoria.

Com Rebus, você pode integrar a sua aplicação com diferentes tipos de sistemas de mensagens, como RabbitMQ, Azure Service Bus, Kafka, entre outros. O Rebus também oferece funcionalidades como:

Retry automático para falhas temporárias.
Dead-letter queues (DLQ) para mensagens que não podem ser processadas.
Auditoria de mensagens.
Configuração e uso simples através de uma API fluente.


## Configuração do Transporte Kafka
```
.Transport(t => t.UseKafka(
    configuration["Kafka:BrokerAddress"],   // Endereço do Kafka Broker
    configuration["Kafka:TopicName"],       // Nome do tópico Kafka
    "RebusPublisher"))                      // Nome do publisher
```
.Transport(t => t.UseKafka(...)): Esta parte configura o transporte de mensagens do Rebus para usar Kafka. Ou seja, ele faz com que o Rebus envie (e/ou consuma) mensagens através do Kafka.
configuration["Kafka:BrokerAddress"]: Aqui você especifica o endereço do broker Kafka. Este valor vem da configuração (geralmente de um arquivo appsettings.json).
configuration["Kafka:TopicName"]: O nome do tópico Kafka que será usado para enviar as mensagens. O Rebus publicará as mensagens neste tópico.
"RebusPublisher": Este é o nome da instância do publisher que o Rebus usará para enviar as mensagens. Ele pode ser configurado como preferir, mas geralmente é um identificador da parte da aplicação que está publicando as mensagens.

## Configuração de Roteamento de Mensagens
```
.Routing(r => r.TypeBased()
    .Map<KafkaModel>(configuration["Kafka:TopicName"]))
```
.Routing(r => r.TypeBased().Map<KafkaModel>(...) ): Aqui, estamos configurando o roteamento de mensagens no Rebus com base no tipo de mensagem. Ou seja, aonde uma mensagem deve ser enviada depende do tipo dela.
r.TypeBased(): Indica que o roteamento será baseado no tipo de mensagem.
.Map<KafkaModel>(configuration["Kafka:TopicName"]): Aqui, estamos dizendo que qualquer mensagem do tipo KafkaModel será roteada para o tópico Kafka especificado pela configuração configuration["Kafka:TopicName"].
Isso permite que você mapeie tipos de mensagens específicas para tópicos Kafka específicos. Caso você tenha múltiplos tipos de mensagens, pode mapear diferentes tópicos para cada tipo de mensagem.

## Configuração da Estratégia de Retry
```
.Options(t => t.RetryStrategy(errorQueueName: "DLQ_" + configuration["Kafka:TopicName"], maxDeliveryAttempts: 5))
```
.Options(t => t.RetryStrategy(...)): Define a estratégia de retry para quando uma mensagem falha ao ser processada. O Rebus oferece várias estratégias de retry que determinam o comportamento quando uma mensagem não pode ser processada.
errorQueueName: "DLQ_" + configuration["Kafka:TopicName"]: O nome da Dead Letter Queue (DLQ), que é onde as mensagens que falham depois de várias tentativas são enviadas. O nome dessa fila é dinâmico, dependendo do nome do tópico Kafka configurado. Por exemplo, se o tópico for meu-topico, a fila de DLQ será chamada DLQ_meu-topico.
maxDeliveryAttempts: 5: O número máximo de tentativas de entrega para uma mensagem antes que ela seja enviada para a DLQ. Se a mensagem falhar mais de 5 vezes, ela será movida para a DLQ.

## Habilitando Auditoria de Mensagens
```
.Options(t => t.EnableMessageAuditing(auditQueue: "Audit_" + configuration["Kafka:TopicName"]))
```
.Options(t => t.EnableMessageAuditing(...)): Esta opção habilita a auditoria de mensagens. Ou seja, o Rebus armazenará informações sobre todas as mensagens publicadas/consumidas para análise posterior (como rastreamento de mensagens).
auditQueue: "Audit_" + configuration["Kafka:TopicName"]: Define o nome da fila de auditoria, que será um tópico separado no Kafka onde as mensagens auditadas serão armazenadas. O nome dessa fila de auditoria será dinâmico, assim como a DLQ, dependendo do nome do tópico Kafka. Por exemplo, se o tópico Kafka for meu-topico, a fila de auditoria será chamada Audit_meu-topico.

## Configuração de Backoff Times
```
.Options(t => t.SetBackoffTimes(new[] {
    TimeSpan.FromSeconds(1),  // 1ª tentativa (1 segundo)
    TimeSpan.FromSeconds(3),  // 2ª tentativa (3 segundos)
    TimeSpan.FromSeconds(5),  // 3ª tentativa (5 segundos)
    TimeSpan.FromSeconds(10), // 4ª tentativa (10 segundos)
    TimeSpan.FromSeconds(15)  // 5ª tentativa (15 segundos)
}))
```
.Options(t => t.SetBackoffTimes(...)): Essa configuração define a estratégia de backoff (tempo de espera entre tentativas de reenvio) em caso de falha.
new[] { TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(3), ... }: Define os tempos de espera entre as tentativas de reenvio. Isso significa que, se uma mensagem falhar, o Rebus aguardará o tempo especificado antes de tentar novamente.
1ª tentativa: Espera 1 segundo.
2ª tentativa: Espera 3 segundos.
3ª tentativa: Espera 5 segundos.
4ª tentativa: Espera 10 segundos.
5ª tentativa: Espera 15 segundos.
Esse comportamento de backoff gradual ajuda a evitar que a aplicação sobrecarregue o sistema com tentativas muito rápidas em caso de falhas temporárias, dando tempo para que o problema seja resolvido.

## Resumo Completo da Configuração
O Rebus está configurado para usar o Kafka como transporte de mensagens, publicando mensagens em um tópico Kafka.
O roteamento é configurado de forma que mensagens do tipo KafkaModel sejam enviadas para um tópico específico.
A estratégia de retry está configurada para tentar 5 vezes antes de mover a mensagem para uma Dead Letter Queue (DLQ).
A auditoria de mensagens está habilitada, o que cria uma fila de auditoria onde informações sobre as mensagens publicadas e consumidas são armazenadas.
O backoff entre as tentativas de reenvio é configurado para aumentar progressivamente o tempo entre cada tentativa, de 1 segundo até 15 segundos.
Quando usar essas configurações?
Essas configurações são úteis quando você tem um sistema de mensagens robusto e precisa de uma solução que lide com falhas, faça retries, armazene mensagens que falharam (na DLQ) e forneça um meio de auditar todas as mensagens que passaram pelo sistema.

## UseKafka e UseKafkaAsOneWayClient
UseKafka: Usado quando você precisa de ambos os lados da comunicação – envio e recebimento de mensagens. Ideal para cenários request-response ou publish-subscribe (publicação e consumo).
UseKafkaAsOneWayClient: Usado quando você deseja apenas enviar mensagens para Kafka de forma unidirecional, sem esperar por resposta ou configurar um consumidor para processar mensagens recebidas.
Se o seu cenário envolve apenas a publicação de mensagens sem necessidade de um consumidor ativo (ou seja, você está enviando eventos ou notificações), então UseKafkaAsOneWayClient é o método mais adequado. Se, por outro lado, você precisa tanto publicar quanto consumir mensagens, então UseKafka