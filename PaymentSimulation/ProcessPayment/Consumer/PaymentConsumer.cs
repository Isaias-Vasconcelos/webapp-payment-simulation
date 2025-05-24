using Contracts.Commands;
using Contracts.Events;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace ProcessPayment.Consumer;
public class PaymentConsumer(IPublishEndpoint publishEndpoint, ILogger<PaymentConsumer> logger): IConsumer<IProcessPayment>
{
    public async Task Consume(ConsumeContext<IProcessPayment> context)
    {
        logger.LogInformation($"[MICROSERVICE] -> Processing payment... [{context.Message.Id}]");
        await publishEndpoint.Publish<IPaymentProcessed>(new
        {
            context.Message.Id,
            context.Message.SocketId,
            context.Message.Email,
            Status = ApprovedRecused(context.Message.Amount),
            context.Message.Amount,
            ProcessedDate = DateTime.UtcNow
        });
        logger.LogInformation($"[MICROSERVICE] -> Processed payment. [{context.Message.Id}]");
    }

    private static string ApprovedRecused(decimal amount)
    {
        return amount <= 2000.00m ? "APPROVED" : "REPROVED";
    }
}