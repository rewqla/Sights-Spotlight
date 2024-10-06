using Microsoft.AspNetCore.Connections;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using StoreDAL.Data;

namespace API.Health
{
    public class DatabaseHealthCheck(StoreContext storeContext, ILogger<DatabaseHealthCheck> logger)
        : IHealthCheck
    {
        public const string Name = "Database";

        public async Task<HealthCheckResult> CheckHealthAsync(
            HealthCheckContext context, CancellationToken cancellationToken = new())
        {
            try
            {
                await storeContext.Database.ExecuteSqlRawAsync("SELECT 1", cancellationToken);
                logger.LogInformation("Database is healthy.");
                return HealthCheckResult.Healthy();
            }
            catch (Exception e)
            {
                const string errorMessage = "Database is unhealthy";
                logger.LogError(e, errorMessage);
                return HealthCheckResult.Unhealthy(errorMessage, e);
            }
        }
    }
}
