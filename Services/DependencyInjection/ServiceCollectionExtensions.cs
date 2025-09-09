using Contracts;
using Microsoft.Extensions.DependencyInjection;
using Services.AccountOperationServices;
using Services.BankAccountServices;
using Services.UsersServices;

namespace Services.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection collection)
    {
        collection.AddScoped<IUserService, UserService>();
        collection.AddScoped<CurrentUserService>();
        collection.AddScoped<ICurrentUserService>(
            p => p.GetRequiredService<CurrentUserService>());
        collection.AddScoped<IAdminService, AdminService>();

        collection.AddScoped<IAccountOperationService, AccountOperationService>();

        collection.AddScoped<IBankAccountService, BankAccountService>();

        collection.AddSingleton<IFrontEndModeService, FrontEndModeService>();

        return collection;
    }
}