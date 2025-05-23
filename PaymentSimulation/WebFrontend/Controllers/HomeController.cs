using System.Diagnostics;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using WebFrontend.Models;
using WebFrontend.Services;

namespace WebFrontend.Controllers;

public class HomeController(ILogger<HomeController> logger, ProductService productService)
    : Controller
{
    private readonly ILogger<HomeController> _logger = logger;
    private readonly ProductService _productService = productService;

    public async Task<IActionResult> Index()
    {
        _logger.LogInformation($"STEP -> Preparing the catalog model");
        var catalog = await _productService.GetCatalogo();
        return View(catalog);
    }

    public async Task<IActionResult> Checkout(Guid productId)
    {
        var itemCheckout = await _productService.CheckoutProduct(productId);
        return View(itemCheckout);
    }

    public async Task<JsonResult> Payment([FromBody] PaymentModel model)
    {
        var itemPayment = await _productService.PaymentProduct(model);
        return Json(itemPayment);
    }
}