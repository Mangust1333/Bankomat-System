using ConsolePresentation.ScenarioMaterial;
using Contracts;
using Model;
using System.Diagnostics.CodeAnalysis;

namespace ConsolePresentation.Scenarios.LoginScenarios;

public class AdminLoginScenarioProvider : IScenarioProvider
{
    private readonly IAdminService _adminService;
    private readonly IFrontEndModeService _modeService;

    public AdminLoginScenarioProvider(IAdminService adminService, IFrontEndModeService modeService)
    {
        _modeService = modeService;
        _adminService = adminService;
    }

    public bool TryGetScenario([NotNullWhen(true)] out IScenario? scenario)
    {
        if (_modeService.Mode is FrontEndModeType.LoginMenu)
        {
            scenario = new AdminLoginScenario(_adminService, _modeService);
            return true;
        }

        scenario = null;
        return false;
    }
}