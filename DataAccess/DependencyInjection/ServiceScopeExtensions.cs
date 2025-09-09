using Itmo.Dev.Platform.Postgres.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace DataAccess.DependencyInjection;

public static class ServiceScopeExtensions
{
    public static void TryUpdateInfrastructureByMigration(this IServiceScope scope)
    {
        scope.UsePlatformMigrationsAsync(default).GetAwaiter().GetResult();
    }
}