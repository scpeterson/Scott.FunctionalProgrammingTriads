using Scott.FunctionalProgrammingTriads.Core.Interfaces;

namespace Scott.FunctionalProgrammingTriads.Core;

public sealed class ConsoleOutput : IStyledOutput
{
    public void WriteLine(string message) => Console.WriteLine(message);

    public void WithColor(ConsoleColor color, Action action)
    {
        var original = Console.ForegroundColor;
        try
        {
            Console.ForegroundColor = color;
            action();
        }
        finally
        {
            Console.ForegroundColor = original;
        }
    }
}
