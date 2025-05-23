namespace WebRestApi.Messaging.RabbitMQ;

public class RabbitMQService:IRabbitMQService
{
    public Task SendCommand<T>(T message)
    {
        throw new NotImplementedException();
    }
}