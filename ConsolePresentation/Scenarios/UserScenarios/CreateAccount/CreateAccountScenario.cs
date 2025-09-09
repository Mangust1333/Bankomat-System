using ConsolePresentation.ScenarioMaterial;
using ConsolePresentation.Scenarios.OkayButton;
using Contracts;
using Model;
using Model.DTO;
using Spectre.Console;

namespace ConsolePresentation.Scenarios.UserScenarios.CreateAccount;

public class CreateAccountScenario(IBankAccountService accountService, IFrontEndModeService modeService, ICurrentUserService currentUserService) : IScenario
{
    public string Name { get; } = "CreateAccount";

    public void Run()
    {
        long userId = currentUserService.CurrentUser?.Id ?? throw new NullReferenceException();
        string name = AnsiConsole.Ask<string>("Name: ");
        decimal balance = AnsiConsole.Ask<decimal>("with balance: ");
        Currency currency = AnsiConsole.Ask<Currency>("Enter currency: ");

        accountService.CreateAccount(new BankAccountDto(name, balance, currency, userId));
        var executor = new OkayButtonExecutor(new OkayButtonScenario(modeService, FrontEndModeType.UserMenu));
        executor.Run("Success!");
    }
}