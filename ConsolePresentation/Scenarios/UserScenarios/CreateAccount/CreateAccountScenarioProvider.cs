using ConsolePresentation.ScenarioMaterial;
using Contracts;
using Model;
using System.Diagnostics.CodeAnalysis;

namespace ConsolePresentation.Scenarios.UserScenarios.CreateAccount;

public class CreateAccountScenarioProvider(IBankAccountService accountService, IFrontEndModeService modeService, ICurrentUserService currentUserService) : IScenarioProvider
{
    public bool TryGetScenario([NotNullWhen(true)] out IScenario? scenario)
    {
        if (modeService.Mode is FrontEndModeType.UserMenu)
        {
            scenario = new CreateAccountScenario(accountService, modeService, currentUserService);
            return true;
        }

        scenario = null;
        return false;
    }
}