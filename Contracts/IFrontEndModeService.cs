using Model;

namespace Contracts;

public interface IFrontEndModeService
{
    FrontEndModeType Mode { get; }

    void ChangeMode(FrontEndModeType newMode);
}