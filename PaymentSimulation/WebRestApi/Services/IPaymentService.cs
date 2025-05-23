using WebRestApi.Dto;

namespace WebRestApi.Services;

public interface IPaymentService
{
    Task<Response<PaymentDto>> CreatePaymentService(PaymentDto payment);
}