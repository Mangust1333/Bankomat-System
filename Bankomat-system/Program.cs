using ConsolePresentation.DependencyInjection;
using ConsolePresentation.ScenarioMaterial;
using DataAccess.DependencyInjection;
using JSONAccess.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Services.DependencyInjection;
using Spectre.Console;

var collection = new ServiceCollection();

collection
    .AddApplication()
    .AddInfrastructureDataAccess(configuration =>
    {
        configuration.Host = "localhost";
        configuration.Port = 54321;
        configuration.Username = "VlaD";
        configuration.Password = "QWERTY";
        configuration.Database = "Bank";
        configuration.SslMode = "Prefer";
    })
    .AddPresentationConsole()
    .AddPasswordInfrastructure("..\\..\\SystemPassword.json");

ServiceProvider provider = collection.BuildServiceProvider();
using IServiceScope scope = provider.CreateScope();

scope.TryUpdateInfrastructureByMigration();

ScenarioRunner scenarioRunner = scope.ServiceProvider
    .GetRequiredService<ScenarioRunner>();

while (true)
{
    scenarioRunner.Run();
    AnsiConsole.Clear();
}