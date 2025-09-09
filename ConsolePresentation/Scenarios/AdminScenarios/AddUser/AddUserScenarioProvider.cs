using ConsolePresentation.ScenarioMaterial;
using Contracts;
using Model;
using System.Diagnostics.CodeAnalysis;

namespace ConsolePresentation.Scenarios.AdminScenarios.AddUser;

public class AddUserScenarioProvider : IScenarioProvider
{
    private readonly IAdminService _adminService;
    private readonly IFrontEndModeService _modeService;

    public AddUserScenarioProvider(IAdminService adminService, IFrontEndModeService modeService)
    {
        _modeService = modeService;
        _adminService = adminService;
    }

    public bool TryGetScenario([NotNullWhen(true)] out IScenario? scenario)
    {
        if (_modeService.Mode is FrontEndModeType.AdminMenu)
        {
            scenario = new AddUserScenario(_adminService, _modeService);
            return true;
        }

        scenario = null;
        return false;
    }
}