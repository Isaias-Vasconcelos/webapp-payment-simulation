using WebRestApi.Dto;

namespace WebRestApi.Repository;

public class ProductRepository(ILogger<ProductRepository> logger) : IProductRepository
{
    private readonly IEnumerable<ProductDto> _products = ProductMock.Products;

    public IEnumerable<ProductDto> GetProductsRepository()
    {
        logger.LogInformation($"STEP [REPOSITORY] -> Searching for products in database fake");
        return _products;
    }

    public ProductDto GetProductRepository(Guid productId)
    {
        logger.LogInformation($"STEP [REPOSITORY] -> Searching for product in database fake [{productId}]");
        var product = _products.FirstOrDefault(p => p.Id == productId);
        return product;
    }
}