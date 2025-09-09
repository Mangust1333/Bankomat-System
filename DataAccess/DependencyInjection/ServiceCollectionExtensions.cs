using Abstractions;
using DataAccess.Migrations;
using DataAccess.Plugins;
using DataAccess.Repository;
using Itmo.Dev.Platform.Common.Extensions;
using Itmo.Dev.Platform.Postgres.Extensions;
using Itmo.Dev.Platform.Postgres.Models;
using Itmo.Dev.Platform.Postgres.Plugins;
using Microsoft.Extensions.DependencyInjection;

namespace DataAccess.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructureDataAccess(
        this IServiceCollection collection,
        Action<PostgresConnectionConfiguration> configuration)
    {
        collection.AddPlatform();
        collection.AddPlatformPostgres(builder => builder.Configure(configuration));
        collection.AddPlatformMigrations(typeof(Initial).Assembly);

        collection.AddSingleton<IDataSourcePlugin, MappingPlugin>();

        collection.AddScoped<IUserRepository, UserRepository>();
        collection.AddScoped<IBankAccountRepository, BankAccountRepository>();
        collection.AddScoped<IAccountOperationRepository, AccountOperationRepository>();

        return collection;
    }
}