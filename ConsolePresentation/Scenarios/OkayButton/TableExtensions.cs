using Spectre.Console;

namespace ConsolePresentation.Scenarios.OkayButton;

public static class TableExtensions
{
    public static string ToPlainText(this Table table)
    {
        var consoleOutput = new StringWriter();
        IAnsiConsole console = AnsiConsole.Create(new AnsiConsoleSettings
        {
            Out = new AnsiConsoleOutput(consoleOutput),
        });

        console.Write(table);

        return consoleOutput.ToString();
    }
}