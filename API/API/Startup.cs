using API.Authorization;
using API.Endpoints;
using API.Health;
using API.Middlewares;
using FluentValidation;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Events;
using StoreBLL.Interfaces;
using StoreBLL.Mappers;
using StoreBLL.Middlewares;
using StoreBLL.Services;
using StoreDAL.Data;
using StoreDAL.Entities;
using StoreDAL.Interfaces;
using StoreDAL.Repository;
using System.Text;
using StoreBLL.Validators;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, loggerConfiguration) =>
{
    loggerConfiguration
        .ReadFrom.Configuration(context.Configuration)
        .WriteTo.Console();
});

var configuration = builder.Configuration;

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    var jwtSecurityScheme = new OpenApiSecurityScheme
    {
        BearerFormat = "JWT",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = JwtBearerDefaults.AuthenticationScheme,
        Description = "Put Bearer [space] and then your token ",

        Reference = new OpenApiReference
        {
            Id = JwtBearerDefaults.AuthenticationScheme,
            Type = ReferenceType.SecurityScheme
        }
    };

    c.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        { jwtSecurityScheme, Array.Empty<string>() }
    });
});

builder.Services.AddDbContext<StoreContext>(opt =>
{
        var auditEntries = new List<AuditEntry>(); 

        opt.UseNpgsql(configuration.GetConnectionString("DefaultConnection"))
            .AddInterceptors(new AuditInterceptor(auditEntries));
});

builder.Services.AddIdentity<User, Role>(options =>
{
    options.User.RequireUniqueEmail = true;
    options.Password.RequiredLength = 8;
    options.Password.RequireDigit = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireLowercase = false;
}).AddEntityFrameworkStores<StoreContext>();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWTSettings:TokenKey"]!))
    };
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy(PolicyClaims.Admin,
        p => p.AddRequirements(new AdminAuthRequirement(configuration["ApiKey"]!)));

    options.AddPolicy(PolicyRoles.Member, policy =>
          policy.RequireClaim(PolicyClaims.ClaimPath, PolicyClaims.Member));
    options.AddPolicy(PolicyRoles.Viewer, policy =>
          policy.RequireClaim(PolicyClaims.ClaimPath, PolicyClaims.Viewer));
});

builder.Services.AddOutputCache(x =>
{
    x.AddBasePolicy(c => c.Cache());
    x.AddPolicy("SightCache", c =>
        c.Cache()
        .Expire(TimeSpan.FromMinutes(1))
        .SetVaryByQuery(new[] { "country", "yearOfFoundationFrom", "YearOfFoundationTo", "page", "pageSize" })
        .Tag("sights"));
});

builder.Services.AddHttpClient();

builder.Services.AddHealthChecks()
     .AddCheck<DatabaseHealthCheck>(DatabaseHealthCheck.Name)
     .AddCheck<RemoteHealthCheck>("Remote endpoints Health Check", failureStatus: HealthStatus.Unhealthy);

builder.Services.AddScoped<IValidator<Country>, CountryValidation>();
builder.Services.AddScoped<ICountryRepository, CountryRepository>();
builder.Services.AddScoped<ISightRepository, SightsRepository>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<ICountryService, CountryService>();
builder.Services.AddScoped<ISightService, SightService>();
builder.Services.AddScoped<ApiKeyAuthFilter>();

builder.Services.AddKeyedScoped<List<AuditEntry>>("Audit");

var app = builder.Build();

// Configure the HTTP request pipeline.
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ValidationMappingMiddleware>();
app.UseMiddleware<RequestLogContextMiddleware>();

app.UseHttpsRedirection();
app.UseRouting();

app.UseCors(opt =>
{
    opt.AllowAnyHeader().AllowAnyMethod().AllowCredentials().WithOrigins("http://localhost:3000");
});

app.UseAuthentication();
app.UseAuthorization();

app.UseOutputCache();

app.UseStaticFiles();
app.UseSerilogRequestLogging();

app.MapApiEndpoints();

using (var scope = app.Services.CreateScope())
{
    var serviceProvider = scope.ServiceProvider;
    var context = serviceProvider.GetRequiredService<StoreContext>();
    var logger = serviceProvider.GetRequiredService<ILogger<Program>>();

    try
    {
        await SeederDB.SeedData(serviceProvider);
        logger.LogInformation("Seeding data to the db");
        await context.Database.MigrateAsync();
        logger.LogInformation("Migrating database");
    }
    catch (Exception ex)
    {
        logger.LogError(ex, "A problem occurred during migration");
    }
}

app.MapHealthChecks("/_health", new HealthCheckOptions
{
    ResponseWriter=UIResponseWriter.WriteHealthCheckUIResponse
});

app.Run();
