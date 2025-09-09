using ConsolePresentation.ScenarioMaterial;
using Contracts;
using Model;
using System.Diagnostics.CodeAnalysis;

namespace ConsolePresentation.Scenarios.LoginScenarios;

public class UserLoginScenarioProvider : IScenarioProvider
{
    private readonly IUserService _service;
    private readonly IFrontEndModeService _modeService;

    public UserLoginScenarioProvider(IUserService service, IFrontEndModeService modeService)
    {
        _service = service;
        _modeService = modeService;
    }

    public bool TryGetScenario([NotNullWhen(true)] out IScenario? scenario)
    {
        if (_modeService.Mode is FrontEndModeType.LoginMenu)
        {
            scenario = new LoginScenario(_service, _modeService);
            return true;
        }

        scenario = null;
        return false;
    }
}