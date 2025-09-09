using ConsolePresentation.ScenarioMaterial;
using Contracts;
using Model;
using System.Diagnostics.CodeAnalysis;

namespace ConsolePresentation.Scenarios.UserScenarios.Logout;

public class UserLogoutScenarioProvider : IScenarioProvider
{
    private readonly IUserService _service;
    private readonly IFrontEndModeService _modeService;

    public UserLogoutScenarioProvider(IUserService service, IFrontEndModeService modeService)
    {
        _service = service;
        _modeService = modeService;
    }

    public bool TryGetScenario([NotNullWhen(true)] out IScenario? scenario)
    {
        if (_modeService.Mode is FrontEndModeType.UserMenu)
        {
            scenario = new LogOutScenario(_service, _modeService);
            return true;
        }

        scenario = null;
        return false;
    }
}