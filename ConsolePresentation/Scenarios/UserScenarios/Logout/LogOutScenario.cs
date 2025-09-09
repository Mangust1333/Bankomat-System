using ConsolePresentation.ScenarioMaterial;
using ConsolePresentation.Scenarios.OkayButton;
using Contracts;
using Model;
using Model.ResultTypes;

namespace ConsolePresentation.Scenarios.UserScenarios.Logout;

public class LogOutScenario : IScenario
{
    private readonly IUserService _userService;
    private readonly IFrontEndModeService _modeService;

    public LogOutScenario(IUserService userService, IFrontEndModeService modeService)
    {
        _userService = userService;
        _modeService = modeService;
    }

    public string Name => "Logout";

    public void Run()
    {
        LogoutResultType result = _userService.Logout();

        string message = result switch
        {
            LogoutResultType.Success => "Successful logout!",
            LogoutResultType.UserAlreadyLoggedOut => "User is already logged out!",
            _ => throw new ArgumentOutOfRangeException(nameof(result)),
        };

        var executor = new OkayButtonExecutor(new OkayButtonScenario(_modeService, FrontEndModeType.LoginMenu));
        executor.Run(message);
    }
}