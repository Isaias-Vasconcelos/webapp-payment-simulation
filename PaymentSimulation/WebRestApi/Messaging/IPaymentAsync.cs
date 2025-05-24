using WebRestApi.Dto;

namespace WebRestApi.Messaging;

public interface IPaymentAsync
{
    Task SendPayment(object message);
}