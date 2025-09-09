using ConsolePresentation.ScenarioMaterial;
using ConsolePresentation.Scenarios.OkayButton;
using Contracts;
using Model;
using Model.ResultTypes;
using Spectre.Console;

namespace ConsolePresentation.Scenarios.AdminScenarios.DeleteUser;

public class DeleteUserScenario(IAdminService adminService, IFrontEndModeService frontEndModeService) : IScenario
{
    public string Name { get; } = "Delete User";

    public void Run()
    {
        long userId = AnsiConsole.Ask<long>("user id: ");
        DeleteTupleFromDataBaseResultType result = adminService.DeleteUser(userId);
        string message = result switch
        {
            DeleteTupleFromDataBaseResultType.Success notFountTuple => "User successfully deleted!",
            DeleteTupleFromDataBaseResultType.NotFountTuple success => "Didn't find user!",
            _ => throw new ArgumentOutOfRangeException(nameof(result)),
        };

        var executor = new OkayButtonExecutor(
            new OkayButtonScenario(frontEndModeService, FrontEndModeType.AdminMenu));
        executor.Run(message);
    }
}