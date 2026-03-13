using Scott.FunctionalProgrammingTriads.Core.Interfaces;

namespace Scott.FunctionalProgrammingTriads.Console.Tests;

public sealed class RecordingOutputSink : IOutput
{
    public List<string> Lines { get; } = [];

    public void WriteLine(string message) => Lines.Add(message);
}
