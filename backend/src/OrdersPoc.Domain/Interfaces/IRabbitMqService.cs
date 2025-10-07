namespace OrdersPoc.Domain.Interfaces;

public interface IRabbitMqService
{
    void PublishMessage<T>(string queueName, T message);
}