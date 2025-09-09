using ConsolePresentation.ScenarioMaterial;
using ConsolePresentation.Scenarios.OkayButton;
using Contracts;
using Model;
using Spectre.Console;
using System.Globalization;

namespace ConsolePresentation.Scenarios.UserScenarios.CheckHistoryOperations;

public class CheckHistoryOperationsScenario(
    IFrontEndModeService frontEndModeService,
    IAccountOperationService accountOperationService) : IScenario
{
    public string Name { get; } = "Check History";

    public void Run()
    {
        long accountId = AnsiConsole.Ask<long>("account id: ");
        var operations = accountOperationService.GetAccountOperations(accountId).ToList();
        if (operations.Count == 0)
        {
            AnsiConsole.MarkupLine("[bold yellow]Your Account Operations:[/]");
            Table table = new Table()
                .AddColumn("Id")
                .AddColumn("Type")
                .AddColumn("Balance");

            foreach (AccountOperation operation in operations)
            {
                table.AddRow(operation.Id.ToString(), operation.Operation.ToString(), operation.Balance.ToString(CultureInfo.InvariantCulture));
            }

            var executor = new OkayButtonExecutor(new OkayButtonScenario(
                frontEndModeService,
                FrontEndModeType.UserMenu));
            executor.Run();
        }
        else
        {
            AnsiConsole.MarkupLine("[bold red]Account has no operations.[/]");
        }
    }
}