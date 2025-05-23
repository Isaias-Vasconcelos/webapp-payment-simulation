using Microsoft.AspNetCore.Mvc;
using WebRestApi.Dto;
using WebRestApi.Services;

namespace WebRestApi.Controllers;
[ApiController]
[Route("api/[controller]/[action]")]
public class PaymentController(IPaymentService paymentService) : ControllerBase
{
    private readonly IPaymentService _paymentService = paymentService;

    [HttpPost]
    public async Task<ActionResult<Response<PaymentDto>>> Post(PaymentDto payment)
    {
        var paymentService = await _paymentService.CreatePaymentService(payment);
        return paymentService;
    }
}