using ConsolePresentation.ScenarioMaterial;
using Contracts;
using Model;
using System.Diagnostics.CodeAnalysis;

namespace ConsolePresentation.Scenarios.AdminScenarios.DeleteUser;

public class DeleteUserScenarioProvider : IScenarioProvider
{
    private readonly IAdminService _adminService;
    private readonly IFrontEndModeService _modeService;

    public DeleteUserScenarioProvider(IAdminService adminService, IFrontEndModeService modeService)
    {
        _modeService = modeService;
        _adminService = adminService;
    }

    public bool TryGetScenario([NotNullWhen(true)] out IScenario? scenario)
    {
        if (_modeService.Mode is FrontEndModeType.AdminMenu)
        {
            scenario = new DeleteUserScenario(_adminService, _modeService);
            return true;
        }

        scenario = null;
        return false;
    }
}