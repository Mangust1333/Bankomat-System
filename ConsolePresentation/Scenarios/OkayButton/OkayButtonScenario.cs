using ConsolePresentation.ScenarioMaterial;
using Contracts;
using Model;

namespace ConsolePresentation.Scenarios.OkayButton;

public class OkayButtonScenario : IScenario
{
    private readonly IFrontEndModeService _frontEndModeService;
    private readonly FrontEndModeType _modeType;

    public OkayButtonScenario(IFrontEndModeService frontEndModeService, FrontEndModeType modeType)
    {
        _frontEndModeService = frontEndModeService;
        _modeType = modeType;
    }

    public string Name { get; } = "Ok";

    public void Run()
    {
        _frontEndModeService.ChangeMode(_modeType);
    }
}