using ConsolePresentation.ScenarioMaterial;
using ConsolePresentation.Scenarios.OkayButton;
using Contracts;
using Model;
using Model.DTO;
using Model.ResultTypes;
using Spectre.Console;

namespace ConsolePresentation.Scenarios.AdminScenarios.AddUser;

public class AddUserScenario(IAdminService adminService, IFrontEndModeService frontEndModeService) : IScenario
{
    public string Name { get; } = "Add User";

    public void Run()
    {
        string name = AnsiConsole.Ask<string>("Name: ");
        string email = AnsiConsole.Ask<string>("Email: ");
        string password = AnsiConsole.Ask<string>("Password: ");
        AddUserResultType result = adminService.CreateUser(new UserDto(name, password, email));

        string message = result switch
        {
            AddUserResultType.Success => "User added successfully!",
            AddUserResultType.UserWithEmailAlreadyExists => "User already exists!",
            _ => throw new ArgumentOutOfRangeException(nameof(result)),
        };

        var executor = new OkayButtonExecutor(
            new OkayButtonScenario(frontEndModeService, FrontEndModeType.AdminMenu));
        executor.Run(message);
    }
}