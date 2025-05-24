using WebRestApi.Dto;
using WebRestApi.Messaging;
using WebRestApi.Repository;

namespace WebRestApi.Services.Implementations;

public class PaymentService(IProductRepository productRepository, IPaymentAsync paymentAsync, ILogger<PaymentService> logger) : IPaymentService
{
    public async Task<Response<PaymentDto>> CreatePaymentService(PaymentDto payment)
    {
        logger.LogInformation("STEP [SERVICE] -> Searching price for product");
        var productPrice = productRepository.GetProductRepository(payment.ProductId);
        
        logger.LogInformation("STEP [SERVICE] -> Sending payment for service process");
        await paymentAsync.SendPayment(new
        {
            Id = Guid.NewGuid(),
            payment.SocketId,
            payment.Card.CardNumber,
            payment.Card.CardName,
            payment.Card.CardExpiry,
            payment.Card.CardCvv,
            Amount = productPrice.Price
        });
        
        return new Response<PaymentDto>(payment);
    }
}