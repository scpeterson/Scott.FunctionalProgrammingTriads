namespace Scott.FunctionalProgrammingTriads.Core.Interfaces;

public interface IStyledOutput : IOutput
{
    void WithColor(ConsoleColor color, Action action);
}
