using WebRestApi.Dto;
using WebRestApi.Repository;

namespace WebRestApi.Services.Implementations;

public class ProductService(IProductRepository productRepository) : IProductService
{
    private readonly IProductRepository _productRepository = productRepository;

    public Response<ProductDto> GetProductsService()
    {
        var products = _productRepository.GetProductsRepository();
        return new Response<ProductDto>(products);
    }

    public Response<ProductDto> GetProductService(Guid productId)
    {
        var product = _productRepository.GetProductRepository(productId);
        return new Response<ProductDto>(product);
    }
}