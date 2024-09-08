using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace API.Health;
public class RemoteHealthCheck : IHealthCheck
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ILogger<RemoteHealthCheck> _logger;
    public RemoteHealthCheck(IHttpClientFactory httpClientFactory, ILogger<RemoteHealthCheck> logger)
    {
        _httpClientFactory = httpClientFactory;
        _logger = logger;
    }

    public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = new CancellationToken())
    {
        using (var httpClient = _httpClientFactory.CreateClient())
        {
            try
            {
                var response = await httpClient.GetAsync("https://github.com/");
                if (response.IsSuccessStatusCode)
                {
                    _logger.LogInformation("Remote endpoint is healthy.");
                    return HealthCheckResult.Healthy($"Remote endpoints is healthy.");
                }

                _logger.LogWarning("Remote endpoint is unhealthy.");
                return HealthCheckResult.Unhealthy("Remote endpoint is unhealthy");
            }
            catch (Exception e)
            {
                const string errorMessage = "Error checking remote endpoint health";
                _logger.LogError(e, errorMessage);
                return HealthCheckResult.Unhealthy(errorMessage, e);
            }
        }
    }
}
