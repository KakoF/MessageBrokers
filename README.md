# Sobre

## RabbitMQ
RabbitMQ é um broker de mensagens amplamente utilizado que implementa o protocolo AMQP (Advanced Message Queuing Protocol). Ele é conhecido por sua simplicidade e flexibilidade, sendo adequado para uma ampla gama de casos de uso, desde sistemas de mensagens simples até arquiteturas complexas de roteamento de mensagens.

## Kafka
Apache Kafka é uma plataforma de streaming de eventos desenvolvida para alta taxa de transferência e processamento em tempo real de dados. Ele é baseado no conceito de log distribuído, onde as mensagens são continuamente adicionadas e persistidas em disco para uso futuro. Kafka é ideal para pipelines de dados em tempo real e arquiteturas orientadas a eventos.

## Comparação: RabbitMQ vs Kafka
### Prós do RabbitMQ:
Simplicidade: Fácil de configurar e usar, com uma curva de aprendizado mais suave.

Flexibilidade: Suporta vários padrões de mensagens, como filas de trabalho, roteamento, tópicos e RPC.

Baixa Latência: Ideal para casos de uso que exigem baixa latência e entrega rápida de mensagens.

Roteamento Avançado: Oferece recursos avançados de roteamento de mensagens com trocas e chaves de roteamento.

### Contras do RabbitMQ:
Escalabilidade Limitada: Pode enfrentar desafios de escalabilidade em cenários de alta taxa de transferência.

Persistência de Mensagens: Embora suporte persistência, não é tão eficiente quanto o Kafka para armazenamento de longo prazo de grandes volumes de dados.

### Prós do Kafka:
Alta Taxa de Transferência: Projetado para lidar com grandes volumes de dados e alta taxa de transferência.

Persistência Durável: Armazena mensagens em disco de forma eficiente, permitindo recuperação e reprocessamento de dados.

Escalabilidade Horizontal: Facilmente escalável distribuindo dados em múltiplos brokers em um cluster.

Processamento em Tempo Real: Ideal para pipelines de dados em tempo real e arquiteturas orientadas a eventos.

### Contras do Kafka:
Complexidade: Mais complexo de configurar e gerenciar em comparação com RabbitMQ.

Latência: Pode ter latência mais alta em comparação com RabbitMQ, especialmente em cenários de baixa taxa de transferência.

Modelo de Consumo: Utiliza um modelo de consumo baseado em pull, que pode não ser ideal para todos os casos de uso.

# Mensagens/Consumidores

## RabbitMQ
Em RabbitMQ, quando um consumidor pega uma mensagem de uma fila, a mensagem é removida da fila. O consumidor deve então reconhecer (acknowledge) explicitamente a mensagem para sinalizar ao RabbitMQ que a mensagem foi processada com sucesso. Se o consumidor falhar antes de reconhecer a mensagem, a mensagem poderá ser reencaminhada (redelivered) para outro consumidor ou devolvida à fila original.

## Kafka
Em Kafka, o comportamento é diferente. As mensagens (ou eventos) não são removidas do tópico quando consumidas. Em vez disso, Kafka mantém um log imutável de eventos que podem ser lidos várias vezes por diferentes consumidores. Cada consumidor mantém um deslocamento (offset) que indica a posição atual no log de eventos. Quando uma mensagem é consumida, o consumidor apenas avança seu deslocamento, mas a mensagem permanece no log para que outros consumidores (ou o mesmo consumidor em uma nova leitura) possam acessar a mensagem novamente.

## Resumo das Diferenças
### RabbitMQ:

A mensagem é removida da fila quando consumida.

O consumidor deve reconhecer explicitamente a mensagem.

Se o consumidor falhar antes de reconhecer, a mensagem pode ser redelivered.

### Kafka:

A mensagem permanece no tópico mesmo após ser consumida.

O consumidor mantém um deslocamento (offset) para acompanhar a posição no log.

Mensagens podem ser lidas várias vezes por diferentes consumidores.

## Prós e Contras
### RabbitMQ
Prós:

Simplicidade no processamento de mensagens.

Adequado para padrões de filas de trabalho e roteamento complexo.

Contras:

Escalabilidade limitada em cenários de alta taxa de transferência.

Não mantém um histórico completo de eventos consumidos.

## Kafka
Prós:

Alta taxa de transferência e persistência durável.

Permite reprocessamento de dados e múltiplos consumidores lendo o mesmo log.

Contras:

Mais complexo de gerenciar e configurar.

Utiliza um modelo de consumo baseado em offset que pode requerer mais atenção ao design do sistema.