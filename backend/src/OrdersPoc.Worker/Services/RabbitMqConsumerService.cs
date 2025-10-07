using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using OrdersPoc.Domain.Events;
using OrdersPoc.Infrastructure.Messaging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace OrdersPoc.Worker.Services;

public class RabbitMqConsumerService : BackgroundService
{
    private readonly ILogger<RabbitMqConsumerService> _logger;
    private readonly IConfiguration _configuration;
    private IConnection? _connection;
    private IModel? _channel;

    public RabbitMqConsumerService(
        ILogger<RabbitMqConsumerService> logger,
        IConfiguration configuration)
    {
        _logger = logger;
        _configuration = configuration;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Worker starting...");

        InitializeRabbitMQ();

        if (_channel == null)
        {
            _logger.LogError("Failed to initialize RabbitMQ channel");
            return;
        }

        var queueName = _configuration["RabbitMQ:QueueName"] ?? "pedidos-queue";

        _channel.QueueDeclare(
            queue: queueName,
            durable: true,
            exclusive: false,
            autoDelete: false,
            arguments: null);

        var consumer = new EventingBasicConsumer(_channel);

        consumer.Received += (model, ea) =>
        {
            try
            {
                var body = ea.Body.ToArray();
                var json = Encoding.UTF8.GetString(body);

                _logger.LogInformation("Message received from [{QueueName}]", queueName);
                _logger.LogDebug("Message content: {Json}", json);

                var message = JsonSerializer.Deserialize<PedidoCriadoMessage>(json);

                if (message != null)
                {
                    ProcessMessage(message);

                    _channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
                    _logger.LogInformation("Message processed successfully: Pedido {NumeroPedido}",
                        message.NumeroPedido);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error processing message");
                _channel.BasicNack(deliveryTag: ea.DeliveryTag, multiple: false, requeue: true);
            }
        };

        _channel.BasicConsume(
            queue: queueName,
            autoAck: false,
            consumer: consumer);

        _logger.LogInformation("Worker started and listening to queue: {QueueName}", queueName);

        while (!stoppingToken.IsCancellationRequested)
        {
            await Task.Delay(1000, stoppingToken);
        }
    }

    private void InitializeRabbitMQ()
    {
        try
        {
            var factory = new ConnectionFactory
            {
                HostName = _configuration["RabbitMQ:Host"] ?? "localhost",
                Port = int.Parse(_configuration["RabbitMQ:Port"] ?? "5672"),
                UserName = _configuration["RabbitMQ:Username"] ?? "guest",
                Password = _configuration["RabbitMQ:Password"] ?? "guest",
                AutomaticRecoveryEnabled = true,
                NetworkRecoveryInterval = TimeSpan.FromSeconds(10)
            };

            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();

            _logger.LogInformation("RabbitMQ connection established");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to connect to RabbitMQ");
        }
    }

    private void ProcessMessage(PedidoCriadoMessage message)
    {
        _logger.LogInformation("Processing order: {NumeroPedido}", message.NumeroPedido);
        _logger.LogInformation("Client: {ClienteNome}", message.ClienteNome);
        _logger.LogInformation("Total: {ValorTotal:C}", message.ValorTotal);
        _logger.LogInformation("Date: {DataCriacao}", message.DataCriacao);

        Thread.Sleep(2000);

        _logger.LogInformation("Order {NumeroPedido} processed successfully!", message.NumeroPedido);
    }

    public override void Dispose()
    {
        _channel?.Close();
        _connection?.Close();
        _logger.LogInformation("RabbitMQ connection closed");
        base.Dispose();
    }
}
