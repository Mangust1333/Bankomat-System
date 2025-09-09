using ConsolePresentation.ScenarioMaterial;
using Contracts;
using Model;
using System.Diagnostics.CodeAnalysis;

namespace ConsolePresentation.Scenarios.UserScenarios.CheckAccounts;

public class CheckAccountsScenarioProvider(IBankAccountService accountService, ICurrentUserService currentUserService, IFrontEndModeService modeService) : IScenarioProvider
{
    public bool TryGetScenario([NotNullWhen(true)] out IScenario? scenario)
    {
        if (modeService.Mode is FrontEndModeType.UserMenu)
        {
            scenario = new CheckAccountsScenario(accountService, currentUserService, modeService);
            return true;
        }

        scenario = null;
        return false;
    }
}