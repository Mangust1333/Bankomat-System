using Contracts;
using Model;

namespace Services;

public class FrontEndModeService : IFrontEndModeService
{
    public FrontEndModeType Mode { get; private set; } = FrontEndModeType.LoginMenu;

    public void ChangeMode(FrontEndModeType newMode)
    {
        Mode = newMode;
    }
}