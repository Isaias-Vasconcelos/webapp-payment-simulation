using Contracts.Events;
using MassTransit;
using Microsoft.AspNetCore.SignalR;
using StatusPayment.SignalR;

namespace StatusPayment.Consumer;

public class PaymentProcessedConsumer(IHubContext<HubMessages> hubContext, ILogger<PaymentProcessedConsumer> logger): IConsumer<IPaymentProcessed>
{
    public async Task Consume(ConsumeContext<IPaymentProcessed> context)
    {
        logger.LogInformation($"STEP [MICROSERVICE] -> Sending status for the payment [{context.Message.Id}]");
        await hubContext.Clients.Client(context.Message.SocketId).SendAsync("PaymentProcessed", new
        {
            context.Message.Id,
            context.Message.Amount,
            context.Message.Status,
            context.Message.ProcessedDate,
        });
    }
}