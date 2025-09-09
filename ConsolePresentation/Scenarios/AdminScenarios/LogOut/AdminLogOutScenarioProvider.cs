using ConsolePresentation.ScenarioMaterial;
using Contracts;
using Model;
using System.Diagnostics.CodeAnalysis;

namespace ConsolePresentation.Scenarios.AdminScenarios.LogOut;

public class AdminLogOutScenarioProvider : IScenarioProvider
{
    private readonly IFrontEndModeService _modeService;

    public AdminLogOutScenarioProvider(IFrontEndModeService modeService)
    {
        _modeService = modeService;
    }

    public bool TryGetScenario([NotNullWhen(true)] out IScenario? scenario)
    {
        if (_modeService.Mode is FrontEndModeType.AdminMenu)
        {
            scenario = new AdminLogOutScenario(_modeService);
            return true;
        }

        scenario = null;
        return false;
    }
}