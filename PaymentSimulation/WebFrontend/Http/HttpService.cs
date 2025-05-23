using System.Text;
using System.Text.Json;
using WebFrontend.Models;

namespace WebFrontend.Http;

public class HttpService(HttpClient httpClient) : IHttpService
{
    public async Task<ResponseModel<T>> GetAsync<T>(string endpoint)
    {
        try
        {
            var response = await httpClient.GetAsync(endpoint);
            var content = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            var data = JsonSerializer.Deserialize<ResponseModel<T>>(content,options);

            return data!;
        }
        catch (Exception ex)
        {
            return new ResponseModel<T>()
            {
                IsSuccess = false,
                Message = ex.Message
            };
        }
    }

    public async Task<ResponseModel<T>> PostAsync<T>(string url, T obj)
    {
        try{
            StringContent json = new(JsonSerializer.Serialize(obj),Encoding.UTF8,"application/json");
            var response = await httpClient.PostAsync(url, json);

            var content = await response.Content.ReadAsStringAsync();
            var data = JsonSerializer.Deserialize<ResponseModel<T>>(content);
            
            return data!;
        }
        catch (Exception ex)
        {
            return new ResponseModel<T>
            {
                IsSuccess = false,
                Message = ex.Message
            };
        }
    }
}