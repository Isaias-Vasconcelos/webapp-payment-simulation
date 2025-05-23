using WebFrontend.Http;
using WebFrontend.Models;

namespace WebFrontend.Services;

public class ProductService(IHttpService httpService)
{
    public async Task<CatalogModel> GetCatalogo()
    {
        try
        {
            var response = await httpService.GetAsync<ProductModel>("/api/product/get");
            return new CatalogModel
            {
                IsSuccess = response.IsSuccess,
                Message = response.Message,
                Products = response.Entities
            };
        }
        catch (Exception e)
        {
            return new CatalogModel
            {
                IsSuccess = false,
                Message = e.Message,
            };
        }
    }

    public async Task<CheckoutModel> CheckoutProduct(Guid productId)
    {
        try
        {
            var response = await GetProduct(productId);
            return new CheckoutModel
            {
                IsSuccess = response.IsSuccess,
                Message = response.Message,
                Product = response.Entities?.FirstOrDefault()
            };
        }
        catch (Exception e)
        {
            return new CheckoutModel
            {
                IsSuccess = false,
                Message = e.Message,
            };
        }
    }

    public async Task<ResponseModel<PaymentModel>> PaymentProduct(PaymentModel paymentModel)
    {
        var product = (await GetProduct(paymentModel.ProductId))
            ?.Entities?.FirstOrDefault();

        if (product is null)
            return new ResponseModel<PaymentModel>
            {
                IsSuccess = false,
                Message = "Product not found",
            };

        paymentModel.Amount = product.Price;
        
        var payment = await httpService.PostAsync<PaymentModel>("/api/payment/post", paymentModel);
        return payment;
    }

    private async Task<ResponseModel<ProductModel>> GetProduct(Guid productId)
    {
        var response = await httpService.GetAsync<ProductModel>($"/api/product/get/{productId}");
        return response;
    }
}