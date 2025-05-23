using System.Text;
using System.Text.Json;
using WebFrontend.Models;

namespace WebFrontend.Http;

public class HttpService(HttpClient httpClient, ILogger<HttpService> logger) : IHttpService
{
    public async Task<ResponseModel<T>> GetAsync<T>(string endpoint)
    {
        try
        {
            logger.LogInformation($"STEP [HTTP SERVICE] -> Sending request [{endpoint}]");
            var response = await httpClient.GetAsync(endpoint);
            var content = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            var data = JsonSerializer.Deserialize<ResponseModel<T>>(content, options);

            return data!;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, $"ERROR [HTTP SERVICE]: Error sending request [{endpoint}]");
            return new ResponseModel<T>()
            {
                IsSuccess = false,
                Message = ex.Message
            };
        }
    }

    public async Task<ResponseModel<T>> PostAsync<T>(string endpoint, StringContent json)
    {
        try
        {
            logger.LogInformation($"STEP [HTTP SERVICE] -> Sending request [{endpoint}]");
            var response = await httpClient.PostAsync(endpoint, json);

            var content = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            var data = JsonSerializer.Deserialize<ResponseModel<T>>(content, options);

            return data!;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, $"ERROR [HTTP SERVICE]: Error sending request [{endpoint}]");
            return new ResponseModel<T>
            {
                IsSuccess = false,
                Message = ex.Message
            };
        }
    }
}