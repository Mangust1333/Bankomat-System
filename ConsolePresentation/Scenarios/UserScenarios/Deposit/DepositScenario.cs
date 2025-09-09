using ConsolePresentation.ScenarioMaterial;
using ConsolePresentation.Scenarios.OkayButton;
using Contracts;
using Model;
using Model.DTO;
using Model.ResultTypes;
using Spectre.Console;

namespace ConsolePresentation.Scenarios.UserScenarios.Deposit;

public class DepositScenario(
    IFrontEndModeService frontEndModeService,
    IBankAccountService bankAccountService,
    IAccountOperationService accountOperationService,
    ICurrentUserService currentUserService) : IScenario
{
    public string Name { get; } = "Deposit";

    public void Run()
    {
        if (currentUserService.CurrentUser == null)
        {
            throw new InvalidOperationException("Current user is not authenticated.");
        }

        long accountId = AnsiConsole.Ask<long>("account Id: ");
        decimal balance = AnsiConsole.Ask<decimal>("balance: ");
        AccountOperationResultType result = bankAccountService.TryDeposit(currentUserService.CurrentUser.Id, accountId, balance);
        var executor = new OkayButtonExecutor(
            new OkayButtonScenario(frontEndModeService, FrontEndModeType.UserMenu));
        switch (result)
        {
            case AccountOperationResultType.Success:
                accountOperationService.CreateNewAccountOperationLog(new AccountOperationDto(
                    accountId,
                    Operation.Deposit,
                    balance));
                executor.Run("Success!");
                break;
            case AccountOperationResultType.AccountDontExists:
                executor.Run("Account Don't Exists!");
                break;
            case AccountOperationResultType.UserDontHaveAccountPermission:
                executor.Run("You don't have permission to deposit this account!");
                break;
            default:
                executor.Run("Failure!");
                break;
        }
    }
}