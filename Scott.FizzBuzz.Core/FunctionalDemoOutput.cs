using LanguageExt;
using Scott.FizzBuzz.Core.Interfaces;
using static LanguageExt.Prelude;

namespace Scott.FizzBuzz.Core;

public static class FunctionalDemoOutput
{
    public static Either<string, Unit> Render<T>(
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

        if (spacingResult.IsLeft)
        {
            return spacingResult;
        }

        return result.Match(
            Right: _ => Right<string, Unit>(Unit.Default),
            Left: error => Left<string, Unit>(error));
    }
}
