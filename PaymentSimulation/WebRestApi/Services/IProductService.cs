using WebRestApi.Dto;

namespace WebRestApi.Services;

public interface IProductService
{
    Response<ProductDto> GetProductsService();
    Response<ProductDto> GetProductService(Guid productId);
}