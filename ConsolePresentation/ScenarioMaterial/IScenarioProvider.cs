using System.Diagnostics.CodeAnalysis;

namespace ConsolePresentation.ScenarioMaterial;

public interface IScenarioProvider
{
    bool TryGetScenario([NotNullWhen(true)] out IScenario? scenario);
}