using WebRestApi.Dto;
using WebRestApi.Repository;

namespace WebRestApi.Services.Implementations;

public class ProductService(IProductRepository productRepository, ILogger<ProductService> logger) : IProductService
{
    private readonly IProductRepository _productRepository = productRepository;

    public Response<ProductDto> GetProductsService()
    {
        logger.LogInformation($"STEP [SERVICE] -> Searching for products in repository");
        var products = _productRepository.GetProductsRepository();
        return new Response<ProductDto>(products);
    }

    public Response<ProductDto> GetProductService(Guid productId)
    {
        logger.LogInformation($"STEP [SERVICE] -> Searching for product in repository [{productId}]");
        var product = _productRepository.GetProductRepository(productId);
        return new Response<ProductDto>(product);
    }
}