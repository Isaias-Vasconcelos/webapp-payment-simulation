using WebRestApi.Dto;

namespace WebRestApi.Repository;

public class ProductRepository : IProductRepository
{
    private readonly IEnumerable<ProductDto> _products = ProductMock.Products;

    public IEnumerable<ProductDto> GetProductsRepository()
    { 
        return _products;
    }

    public ProductDto GetProductRepository(Guid productId)
    {
        var product = _products.FirstOrDefault(p => p.Id == productId);
        return product;
    }
}