using ConsolePresentation.ScenarioMaterial;
using ConsolePresentation.Scenarios.OkayButton;
using Contracts;
using Model;
using Spectre.Console;
using System.Globalization;

namespace ConsolePresentation.Scenarios.UserScenarios.CheckAccounts;

public class CheckAccountsScenario(IBankAccountService accountService, ICurrentUserService currentUserService, IFrontEndModeService modeService) : IScenario
{
    public string Name => "Show Accounts";

    public void Run()
    {
        if (currentUserService.CurrentUser is not null)
        {
            long userId = currentUserService.CurrentUser.Id;

            IEnumerable<BankAccount> accounts = accountService.GetUserAccounts(userId).ToList();

            if (accounts.Any())
            {
                AnsiConsole.MarkupLine("[bold yellow]Your Bank Accounts:[/]");
                Table table = new Table()
                    .AddColumn("Account ID")
                    .AddColumn("Name")
                    .AddColumn("Balance")
                    .AddColumn("Currency");

                foreach (BankAccount account in accounts)
                {
                    table.AddRow(account.Id.ToString(), account.Name, account.Balance.ToString(CultureInfo.InvariantCulture), account.AccountCurrency.ToString());
                }

                var executor = new OkayButtonExecutor(new OkayButtonScenario(modeService, FrontEndModeType.UserMenu));
                executor.Run(table);
            }
            else
            {
                AnsiConsole.MarkupLine("[bold red]You have no bank accounts.[/]");
            }
        }
    }
}