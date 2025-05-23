using System.Text.Json;
using System.Text;
using WebFrontend.Http;
using WebFrontend.Models;

namespace WebFrontend.Services;

public class ProductService(IHttpService httpService, ILogger<ProductService> logger)
{
    public async Task<CatalogModel> GetCatalogo()
    {
        try
        {
            logger.LogInformation("STEP [SERVICE] -> Searching for items in the API");
            var response = await GetProducts();
            return new CatalogModel
            {
                IsSuccess = response.IsSuccess,
                Message = response.Message,
                Products = response.Entities
            };
        }
        catch (Exception e)
        {
            logger.LogError(e, "ERROR [SERVICE] -> Error fetching items for Catalog");
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
            logger.LogInformation($"STEP [SERVICE] -> Fetching product information for checkout [{productId}]");
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
            logger.LogError(e, $"ERROR [SERVICE] -> Error fetching product information for checkout");
            return new CheckoutModel
            {
                IsSuccess = false,
                Message = e.Message,
            };
        }
    }

    public async Task<ResponseModel<PaymentModel>> PaymentProduct(PaymentModel paymentModel)
    {
        logger.LogInformation($"STEP [SERVICE] -> Sending informations for the payment [{paymentModel.ProductId}]");
        var product = (await GetProduct(paymentModel.ProductId))
            ?.Entities?.FirstOrDefault();

        if (product is null)
        {
            logger.LogError($"ERROR [SERVICE] -> Product not found [{paymentModel.ProductId}]");
            return new ResponseModel<PaymentModel>
            {
                IsSuccess = false,
                Message = "Product not found",
            };
        }

        StringContent json = new(JsonSerializer.Serialize(paymentModel), Encoding.UTF8, "application/json");

        var payment = await httpService.PostAsync<PaymentModel>("/api/payment/post", json);
        if (payment.IsSuccess)
            logger.LogInformation($"STEP [SERVICE] -> Payment sending [{paymentModel.ProductId}]");
        else
            logger.LogError($"ERROR [SERVICE] -> Error sending payment [{paymentModel.ProductId}] ({payment.Message})");
        return payment;
    }

    private async Task<ResponseModel<ProductModel>> GetProduct(Guid productId)
    {
        logger.LogInformation($"STEP [SERVICE] -> Searching for product information in the API [{productId}]");
        var response = await httpService.GetAsync<ProductModel>($"/api/product/get/{productId}");
        if (response.IsSuccess)
            logger.LogInformation($"STEP [SERVICE] -> Information successfully fetched for the product [{productId}]");
        else
            logger.LogError($"ERROR [SERVICE] -> Error fetching product information from the API [{productId}] ({response.Message})");
        return response;
    }

    private async Task<ResponseModel<ProductModel>> GetProducts()
    {
        logger.LogInformation("STEP [SERVICE] -> Searching for products in the API");
        var response = await httpService.GetAsync<ProductModel>("/api/product/get");
        if (response.IsSuccess)
            logger.LogInformation("STEP [SERVICE] -> Products returned successfully");
        else
            logger.LogError($"ERROR [SERVICE] -> Error fetching products from API ({response.Message})");
        return response;
    }
}