using OrdersPoc.Infrastructure;
using OrdersPoc.Worker.Services;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddHostedService<RabbitMqConsumerService>();

var host = builder.Build();
host.Run();