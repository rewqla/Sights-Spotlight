using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StoreDAL.Data;
using StoreDAL.Entities;
using StoreDAL.Interfaces;
using StoreDAL.Repository;

namespace StoreDAL;

public static class DependencyInjection
{
    public static IServiceCollection AddDataAccess(this IServiceCollection services, IConfiguration configuration)
    {
        return services
            .AddRepositories(configuration)
            .AddDatabase(configuration)
            .AddIdentity(configuration)
            .AddAudit(configuration);
    }

    private static IServiceCollection AddRepositories(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddScoped<ICountryRepository, CountryRepository>()
            .AddScoped<ISightRepository, SightsRepository>();

        return services;
    }

    private static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<StoreContext>(opt =>
        {
            var auditEntries = new List<AuditEntry>();
            opt.UseNpgsql(configuration.GetConnectionString("DefaultConnection"))
                .AddInterceptors(new AuditInterceptor(auditEntries));
        });
        
        return services;
    }
    
    private static IServiceCollection AddAudit(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddKeyedScoped<List<AuditEntry>>("Audit");
        
        return services;
    }
    
    private static IServiceCollection AddIdentity(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddIdentity<User, Role>(options =>
        {
            options.User.RequireUniqueEmail = true;
            options.Password.RequiredLength = 8;
            options.Password.RequireDigit = false;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireUppercase = false;
            options.Password.RequireLowercase = false;
        }).AddEntityFrameworkStores<StoreContext>();

        return services;
    }
}