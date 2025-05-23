using WebFrontend.Http;
using WebFrontend.Services;

namespace WebFrontend.Extensions;

public static class Configuration
{
    public static void ConfigureServices(this IServiceCollection services)
    {
        services.AddHttpClient<IHttpService, HttpService>(p =>
        {
            p.BaseAddress = new Uri("http://localhost:5131");
            p.DefaultRequestHeaders.Add("Accept", "application/json");
        });
        
        services.AddScoped<ProductService>();
    }
}