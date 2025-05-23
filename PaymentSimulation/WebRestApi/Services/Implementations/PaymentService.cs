using WebRestApi.Dto;

namespace WebRestApi.Services.Implementations;

public class PaymentService:IPaymentService
{
    public async Task<Response<PaymentDto>> CreatePaymentService(PaymentDto payment)
    {
        return new Response<PaymentDto>(payment);
    }
}