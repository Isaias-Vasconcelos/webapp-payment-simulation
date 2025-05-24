using Contracts.Commands;
using MassTransit;

namespace WebRestApi.Messaging;

public class PaymentAsync(ISendEndpointProvider provider):IPaymentAsync
{
    public async Task SendPayment(object message)
    {
        var endpoint = await provider.GetSendEndpoint(new Uri("queue:process-payment"));
        await endpoint.Send<IProcessPayment>(message);
    }
}