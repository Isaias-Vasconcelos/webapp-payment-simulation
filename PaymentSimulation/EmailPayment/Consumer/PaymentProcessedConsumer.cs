using Contracts.Events;
using EmailPayment.Service;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace EmailPayment.Consumer;

public class PaymentProcessedConsumer(ILogger<PaymentProcessedConsumer> logger):IConsumer<IPaymentProcessed>
{
    public async Task Consume(ConsumeContext<IPaymentProcessed> context)
    {
        await EmailService.SendEmail(context.Message, logger);
    }
}