using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace API.Health;

public class RemoteHealthCheck(IHttpClientFactory httpClientFactory, ILogger<RemoteHealthCheck> logger)
    : IHealthCheck
{
    public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context,
        CancellationToken cancellationToken = new CancellationToken())
    {
        using var httpClient = httpClientFactory.CreateClient();
        
        try
        {
            var response = await httpClient.GetAsync("https://github.com/", cancellationToken);
            if (response.IsSuccessStatusCode)
            {
                logger.LogInformation("Remote endpoint is healthy.");
                return HealthCheckResult.Healthy($"Remote endpoints is healthy.");
            }

            logger.LogWarning("Remote endpoint is unhealthy.");
            return HealthCheckResult.Unhealthy("Remote endpoint is unhealthy");
        }
        catch (Exception e)
        {
            const string errorMessage = "Error checking remote endpoint health";
            logger.LogError(e, errorMessage);
            return HealthCheckResult.Unhealthy(errorMessage, e);
        }
    }
}