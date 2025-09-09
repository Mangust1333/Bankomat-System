using ConsolePresentation.ScenarioMaterial;
using Contracts;
using Model;
using Model.ResultTypes;
using Spectre.Console;

namespace ConsolePresentation.Scenarios.LoginScenarios;

public class LoginScenario : IScenario
{
    private readonly IUserService _userService;
    private readonly IFrontEndModeService _modeService;

    public LoginScenario(IUserService userService, IFrontEndModeService modeService)
    {
        _userService = userService;
        _modeService = modeService;
    }

    public string Name => "Login";

    public void Run()
    {
        string username = AnsiConsole.Ask<string>("Enter your email address: ");
        string password = AnsiConsole.Prompt(
            new TextPrompt<string>("Enter password:")
                .Secret());

        LoginResultType result = _userService.Login(username, password);

        if (result is LoginResultType.Success)
        {
            _modeService.ChangeMode(FrontEndModeType.UserMenu);
            return;
        }

        string message = result switch
        {
            LoginResultType.UserNotFound => "User not found",
            LoginResultType.WrongPassword => "Wrong password",
            LoginResultType.UserAlreadyLoggedIn => "User is already logged in",
            _ => throw new ArgumentOutOfRangeException(nameof(result)),
        };

        AnsiConsole.WriteLine(message);
        Environment.Exit(0);
    }
}