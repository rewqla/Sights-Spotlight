using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StoreBLL.Handlers;
using StoreBLL.Services;
using StoreBLL.Interfaces;
using StoreBLL.Validators;
using StoreDAL.Entities;

namespace StoreBLL;

public static class DependencyInjection
{
    public static IServiceCollection AddBusinessLogic(this IServiceCollection services, IConfiguration configuration)
    {
        return services
            .AddServices(configuration)
            .AddValidators(configuration)
            .AddExceptionHandlers(configuration)
            .AddCaching(configuration);
    }

    private static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddScoped<ITokenService, TokenService>()
            .AddScoped<ICountryService, CountryService>()
            .AddScoped<ISightService, SightService>();
        
        return services;
    }
    
    private static IServiceCollection AddExceptionHandlers(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddExceptionHandler<BadRequestExceptionHandler>()
            .AddExceptionHandler<NotFoundExceptionHandler>()
            .AddProblemDetails();
        
        return services;
    }
    
    private static IServiceCollection AddValidators(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddScoped<IValidator<Country>, CountryValidation>();

        return services;
    }
    
    private static IServiceCollection AddCaching(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddOutputCache(x =>
            {
                x.AddBasePolicy(c => c.Cache());
                x.AddPolicy("SightCache", c =>
                    c.Cache()
                        .Expire(TimeSpan.FromMinutes(1))
                        .SetVaryByQuery(new[] { "country", "yearOfFoundationFrom", "YearOfFoundationTo", "page", "pageSize" })
                        .Tag("sights"));
            });

        return services;
    }
}