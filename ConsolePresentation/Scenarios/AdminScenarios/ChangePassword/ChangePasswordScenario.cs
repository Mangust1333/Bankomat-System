using ConsolePresentation.ScenarioMaterial;
using ConsolePresentation.Scenarios.OkayButton;
using Contracts;
using Model;
using Model.ResultTypes;
using Spectre.Console;

namespace ConsolePresentation.Scenarios.AdminScenarios.ChangePassword;

public class ChangePasswordScenario(IAdminService adminService, IFrontEndModeService frontEndModeService) : IScenario
{
    public string Name => "Change password";

    public void Run()
    {
        string password = AnsiConsole.Ask<string>("Password: ");
        ChangeAdminPassword result = adminService.ChangePassword(password);

        string message = result switch
        {
            ChangeAdminPassword.Success => "Password changed successfully!",
            ChangeAdminPassword.PasswordDidNotValid => "Invalid password!",
            ChangeAdminPassword.PasswordDidNotFount => "Password did not fount!",
            ChangeAdminPassword.Fail fail => fail.What,
            _ => throw new ArgumentOutOfRangeException(nameof(result)),
        };

        var executor = new OkayButtonExecutor(
            new OkayButtonScenario(frontEndModeService, FrontEndModeType.AdminMenu));
        executor.Run(message);
    }
}