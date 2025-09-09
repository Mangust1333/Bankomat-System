using ConsolePresentation.ScenarioMaterial;
using Contracts;
using Model;
using Model.ResultTypes;
using Spectre.Console;

namespace ConsolePresentation.Scenarios.LoginScenarios;

public class AdminLoginScenario(IAdminService adminService, IFrontEndModeService frontEndModeService) : IScenario
{
    public string Name { get; } = "Admin";

    public void Run()
    {
        string password = AnsiConsole.Prompt(
            new TextPrompt<string>("Enter password:")
                .Secret());

        LoginResultType result = adminService.Login(password);

        if (result is LoginResultType.Success)
        {
            frontEndModeService.ChangeMode(FrontEndModeType.AdminMenu);
            return;
        }

        string message = result switch
        {
            LoginResultType.WrongPassword => "Wrong password",
            _ => throw new ArgumentOutOfRangeException(nameof(result)),
        };

        AnsiConsole.WriteLine(message);
        Environment.Exit(0);
    }
}