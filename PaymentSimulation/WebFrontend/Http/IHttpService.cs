using WebFrontend.Models;

namespace WebFrontend.Http;

public interface IHttpService
{
    Task<ResponseModel<T>> GetAsync<T>(string enpoint);
    Task<ResponseModel<T>> PostAsync<T>(string url,StringContent obj);
}