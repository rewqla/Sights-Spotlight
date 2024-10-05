using API.Authorization;
using API.Endpoints;
using API.Health;
using API.Middlewares;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Serilog;
using StoreBLL.Middlewares;
using StoreDAL.Data;
using StoreDAL.Entities;
using System.Text.Json.Serialization;
using API;
using StoreBLL;
using StoreDAL;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, loggerConfiguration) =>
{
    loggerConfiguration
        .ReadFrom.Configuration(context.Configuration)
        .WriteTo.Console();
});

var configuration = builder.Configuration;

builder.Services
    .AddDataAccess(builder.Configuration)
    .AddBusinessLogic(builder.Configuration)
    .AddPresentation(builder.Configuration);

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

builder.Services.AddHttpClient();

builder.Services.AddHealthChecks()
     .AddCheck<DatabaseHealthCheck>(DatabaseHealthCheck.Name)
     .AddCheck<RemoteHealthCheck>("Remote endpoints Health Check", failureStatus: HealthStatus.Unhealthy);

builder.Services.AddScoped<ApiKeyAuthFilter>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
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
