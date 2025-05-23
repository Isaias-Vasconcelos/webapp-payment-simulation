using FluentValidation;
using WebRestApi.Messaging.RabbitMQ;
using WebRestApi.Repository;
using WebRestApi.Services;
using WebRestApi.Services.Implementations;
using WebRestApi.Validators;

namespace WebRestApi.Extensions;

public static class Configuration
{
    public static void ConfigureServices(this IServiceCollection services)
    {
        services
            .AddServices()
            .AddRepositories()
            .AddEventBus()
            .AddValidators();
    }

    private static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IProductService, ProductService>()
            .AddScoped<IPaymentService, PaymentService>();
        
        return services;
    }

    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IProductRepository, ProductRepository>();
        
        return services;
    }

    private static IServiceCollection AddEventBus(this IServiceCollection services)
    {
        services
            .AddScoped<IRabbitMQService, RabbitMQService>();
        return services;
    }

    private static IServiceCollection AddValidators(this IServiceCollection services)
    {
        services.AddValidatorsFromAssemblyContaining<PaymentValidator>();
        return services;
    }
}