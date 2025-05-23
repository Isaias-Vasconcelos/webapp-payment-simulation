namespace WebRestApi.Messaging.RabbitMQ;

public interface IRabbitMQService
{
    Task SendCommand<T>(T message);
}