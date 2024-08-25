using Microsoft.AspNetCore.Connections;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using StoreDAL.Data;

namespace API.Health
{
    public class DatabaseHealthCheck : IHealthCheck
    {
        public const string Name = "Database";

        private readonly StoreContext _storeContext;
        private readonly ILogger<DatabaseHealthCheck> _logger;

        public DatabaseHealthCheck(StoreContext storeContext, ILogger<DatabaseHealthCheck> logger)
        {
            _storeContext = storeContext;
            _logger = logger;
        }

        public async Task<HealthCheckResult> CheckHealthAsync(
            HealthCheckContext context, CancellationToken cancellationToken = new())
        {
            try
            {
                await _storeContext.Database.ExecuteSqlRawAsync("SELECT 1", cancellationToken);
                return HealthCheckResult.Healthy();
            }
            catch (Exception e)
            {
                const string errorMessage = "Database is unhealthy";
                _logger.LogError(e, errorMessage);
                return HealthCheckResult.Unhealthy(errorMessage, e);
            }
        }
    }
}
