using WebRestApi.Dto;

namespace WebRestApi.Repository;

public interface IProductRepository
{
    IEnumerable<ProductDto> GetProductsRepository();
    ProductDto GetProductRepository(Guid productId);
}