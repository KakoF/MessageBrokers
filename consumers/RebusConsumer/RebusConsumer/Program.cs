using Microsoft.Extensions.Configuration;
using RebusConsumer;
using RebusConsumer.Extensions.BuilderExtensions;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddServiceRebus(builder.Configuration);
builder.Services.AddHostedService<Worker>();

var host = builder.Build();
host.Run();
