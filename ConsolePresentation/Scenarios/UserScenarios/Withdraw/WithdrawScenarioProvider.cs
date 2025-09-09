using ConsolePresentation.ScenarioMaterial;
using Contracts;
using Model;
using System.Diagnostics.CodeAnalysis;

namespace ConsolePresentation.Scenarios.UserScenarios.Withdraw;

public class WithdrawScenarioProvider(
    IFrontEndModeService modeService,
    IBankAccountService accountService,
    IAccountOperationService accountOperationService,
    ICurrentUserService currentUserService) : IScenarioProvider
{
    public bool TryGetScenario([NotNullWhen(true)] out IScenario? scenario)
    {
        if (modeService.Mode is FrontEndModeType.UserMenu)
        {
            scenario = new WithdrawScenario(
                modeService,
                accountService,
                accountOperationService,
                currentUserService);
            return true;
        }

        scenario = null;
        return false;
    }
}