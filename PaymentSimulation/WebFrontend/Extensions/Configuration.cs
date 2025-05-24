using WebFrontend.Http;
using WebFrontend.Services;

namespace WebFrontend.Extensions;

public static class Configuration
{
    public static void ConfigureServices(this IServiceCollection services)
    {
        services.AddHttpClient<IHttpService, HttpService>(p =>
        {
            p.BaseAddress = new Uri(Environment.GetEnvironmentVariable("BACKEND_URL") ?? "http://localhost:5000");
            p.DefaultRequestHeaders.Add("Accept", "application/json");
        });
        
        services.AddScoped<ProductService>();
    }
}