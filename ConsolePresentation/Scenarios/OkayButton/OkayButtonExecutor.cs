using ConsolePresentation.ScenarioMaterial;
using Spectre.Console;

namespace ConsolePresentation.Scenarios.OkayButton;

public class OkayButtonExecutor
{
    private readonly IScenario _scenario;

    public OkayButtonExecutor(OkayButtonScenario scenario)
    {
        _scenario = scenario;
    }

    public void Run(string message = "")
    {
        var selector = new SelectionPrompt<IScenario>();
        if (!string.IsNullOrEmpty(message))
        {
            selector.Title(message);
        }

        selector.AddChoices(_scenario)
            .UseConverter(x => x.Name);

        IScenario scenario = AnsiConsole.Prompt(selector);
        scenario.Run();
    }

    public void Run(Table table)
    {
        string title = GenerateTitleWithTable(table);

        SelectionPrompt<IScenario> selector = new SelectionPrompt<IScenario>()
            .Title(title)
            .AddChoices(_scenario)
            .UseConverter(x => x.Name);

        IScenario scenario = AnsiConsole.Prompt(selector);
        scenario.Run();
    }

    private static string GenerateTitleWithTable(Table? table)
    {
        return table == null ? string.Empty : table.ToPlainText();
    }
}
