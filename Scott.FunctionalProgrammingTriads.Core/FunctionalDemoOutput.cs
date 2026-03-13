using LanguageExt;
using Scott.FunctionalProgrammingTriads.Core.Interfaces;

namespace Scott.FunctionalProgrammingTriads.Core;

public static class FunctionalDemoOutput
{
    public static DemoExecutionResult Render<T>(
        IOutput output,
        string title,
        Either<string, T> result,
        Action<IOutput, T> renderSuccess)
    {
        var spacingResult = OutputUtilities.ExecuteWithSpacing(output, () =>
            {
                result.Match(
                    Right: success => renderSuccess(output, success),
                    Left: error => output.WriteLine($"Failed: {error}"));
            }, title);

        if (!spacingResult.IsSuccess)
        {
            return spacingResult;
        }

        return result.Match(
            Right: _ => DemoExecutionResult.Success(),
            Left: DemoExecutionResult.Failure);
    }
}
