using Abstractions;
using JSONAccess.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace JSONAccess.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddPasswordInfrastructure(
        this IServiceCollection collection,
        string path)
    {
        collection.AddScoped<ISystemPasswordRepository, SystemPasswordRepository>(
            provider => new SystemPasswordRepository(path));
        return collection;
    }
}