using ConsolePresentation.ScenarioMaterial;
using Contracts;
using Model;
using System.Diagnostics.CodeAnalysis;

namespace ConsolePresentation.Scenarios.UserScenarios.CheckHistoryOperations;

public class CheckHistoryOperationsScenarioProvider(
    IFrontEndModeService frontEndModeService,
    IAccountOperationService accountOperationService) : IScenarioProvider
{
    public bool TryGetScenario([NotNullWhen(true)] out IScenario? scenario)
    {
        if (frontEndModeService.Mode is FrontEndModeType.UserMenu)
        {
            scenario = new CheckHistoryOperationsScenario(frontEndModeService, accountOperationService);
            return true;
        }

        scenario = null;
        return false;
    }
}