using Microsoft.AspNetCore.Mvc;
using WebRestApi.Dto;
using WebRestApi.Services;

namespace WebRestApi.Controllers;
[ApiController]
[Route("api/[controller]/[action]")]
public class ProductController(IProductService productService) : ControllerBase
{
    private readonly IProductService _productService = productService;

    [HttpGet]
    public ActionResult<Response<ProductDto>> Get()
    {
        var products = _productService.GetProductsService();
        return products;
    }

    [HttpGet("{id:guid}")]
    public ActionResult<Response<ProductDto>> Get(Guid id)
    {
        var product = _productService.GetProductService(id);
        return product;
    }
}