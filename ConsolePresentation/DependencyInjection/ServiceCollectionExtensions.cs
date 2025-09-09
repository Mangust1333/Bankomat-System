using ConsolePresentation.ScenarioMaterial;
using ConsolePresentation.Scenarios.AdminScenarios.AddUser;
using ConsolePresentation.Scenarios.AdminScenarios.ChangePassword;
using ConsolePresentation.Scenarios.AdminScenarios.DeleteUser;
using ConsolePresentation.Scenarios.AdminScenarios.LogOut;
using ConsolePresentation.Scenarios.LoginScenarios;
using ConsolePresentation.Scenarios.UserScenarios.CheckAccounts;
using ConsolePresentation.Scenarios.UserScenarios.CheckHistoryOperations;
using ConsolePresentation.Scenarios.UserScenarios.CreateAccount;
using ConsolePresentation.Scenarios.UserScenarios.Deposit;
using ConsolePresentation.Scenarios.UserScenarios.Logout;
using ConsolePresentation.Scenarios.UserScenarios.Withdraw;
using Microsoft.Extensions.DependencyInjection;

namespace ConsolePresentation.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddPresentationConsole(this IServiceCollection collection)
    {
        collection.AddScoped<ScenarioRunner>();

        collection.AddScoped<IScenarioProvider, UserLoginScenarioProvider>();
        collection.AddScoped<IScenarioProvider, AdminLoginScenarioProvider>();
        collection.AddScoped<IScenarioProvider, UserLogoutScenarioProvider>();
        collection.AddScoped<IScenarioProvider, AddUserScenarioProvider>();
        collection.AddScoped<IScenarioProvider, DeleteUserScenarioProvider>();
        collection.AddScoped<IScenarioProvider, ChangePasswordScenarioProvider>();
        collection.AddScoped<IScenarioProvider, AdminLogOutScenarioProvider>();
        collection.AddScoped<IScenarioProvider, CheckAccountsScenarioProvider>();
        collection.AddScoped<IScenarioProvider, CreateAccountScenarioProvider>();
        collection.AddScoped<IScenarioProvider, CheckHistoryOperationsScenarioProvider>();
        collection.AddScoped<IScenarioProvider, DepositScenarioProvider>();
        collection.AddScoped<IScenarioProvider, WithdrawScenarioProvider>();

        return collection;
    }
}