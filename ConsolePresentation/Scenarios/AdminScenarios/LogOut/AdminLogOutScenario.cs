using ConsolePresentation.ScenarioMaterial;
using Contracts;
using Model;

namespace ConsolePresentation.Scenarios.AdminScenarios.LogOut;

public class AdminLogOutScenario(IFrontEndModeService modeService) : IScenario
{
    public string Name => "Exit";

    public void Run()
    {
        modeService.ChangeMode(FrontEndModeType.LoginMenu);
    }
}