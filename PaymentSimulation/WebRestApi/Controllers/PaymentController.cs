using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using WebRestApi.Dto;
using WebRestApi.Services;

namespace WebRestApi.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class PaymentController(IPaymentService paymentService, IValidator<PaymentDto> validator, ILogger<PaymentController> logger) : ControllerBase
{
    private readonly IPaymentService _paymentService = paymentService;

    [HttpPost]
    public async Task<ActionResult<Response<PaymentDto>>> Post([FromBody] PaymentDto payment)
    {
        logger.LogInformation($"STEP [CONTROLLER] -> Creating payment for product [{payment.ProductId}]");
        var validatorPayment = await validator.ValidateAsync(payment);

        if (!validatorPayment.IsValid)
        {
            logger.LogError($"ERROR [CONTROLLER] -> Check the fields! [{payment.ProductId}]");
            foreach (var e in validatorPayment.Errors)
            {
                Console.WriteLine(e.ErrorMessage);
            }
            return BadRequest(new Response<PaymentDto>("Error! Check the fields!"));
        }

        var paymentService = await _paymentService.CreatePaymentService(payment);
        logger.LogInformation($"STEP [CONTROLLER] -> Payment sending for the product [{payment.ProductId}]");

        return paymentService;
    }
}