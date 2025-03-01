using Rebus.Config;
using RebusPublisher.Extensions.AppExtensions;
using RebusPublisher.Extensions.BuilderExtensions;

var builder = WebApplication.CreateBuilder(args);

/*var kafkaInitializer = new KafkaInitializer(builder.Configuration["Kafka:BrokerAddress"]!);
await kafkaInitializer.EnsureTopicExistsAsync(builder.Configuration["Kafka:TopicName"]!);*/

builder.Services.AddControllers();

builder.Services.AddSwagger();
builder.Services.AddServiceRebus(builder.Configuration);
builder.Services.AddDomainExtensions(builder.Configuration);

var app = builder.Build();

app.ConfigureSwagger(app.Environment);

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
