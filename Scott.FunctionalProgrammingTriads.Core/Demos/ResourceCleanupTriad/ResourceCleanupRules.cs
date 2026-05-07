using LanguageExt;
using static LanguageExt.Prelude;

namespace Scott.FunctionalProgrammingTriads.Core.Demos.ResourceCleanupTriad;

public static class ResourceCleanupRules
{
    public sealed record CleanupScenario(string Name, bool ShouldFail);

    public sealed record ResourceCleanupResult(bool Success, string Message, IReadOnlyList<string> Trace);

    public static bool TryResolveScenario(string? name, out CleanupScenario? scenario, out string? error)
    {
        switch ((name ?? string.Empty).Trim().ToLowerInvariant())
        {
            case "":
            case "success":
            case "ok":
                scenario = new CleanupScenario("success", ShouldFail: false);
                error = null;
                return true;
            case "fail":
            case "failure":
                scenario = new CleanupScenario("fail", ShouldFail: true);
                error = null;
                return true;
            default:
                scenario = null;
                error = "Scenario must be one of: success|ok|fail|failure.";
                return false;
        }
    }

    public static ResourceCleanupResult ExecuteImperative(CleanupScenario scenario)
    {
        var trace = new List<string>();
        TraceResource? resource = null;
        var success = false;
        var message = FailureMessage;

        try
        {
            resource = Acquire(trace);
            resource.Write("begin");

            if (scenario.ShouldFail)
            {
                throw new InvalidOperationException(FailureMessage);
            }

            resource.Write("complete");
            success = true;
            message = SuccessMessage;
        }
        catch (InvalidOperationException ex)
        {
            message = ex.Message;
        }
        finally
        {
            resource?.Dispose();
        }

        return Complete(trace, success, message);
    }

    public static ResourceCleanupResult ExecuteCSharpPipeline(CleanupScenario scenario)
    {
        var trace = new List<string>();

        var outcome = WithResource(trace, resource =>
            StepResult.Succeed()
                .Tap(() => resource.Write("begin"))
                .Bind(() => scenario.ShouldFail
                    ? StepResult.Fail(FailureMessage)
                    : StepResult.Succeed())
                .Tap(() => resource.Write("complete"))
                .Match(
                    onSuccess: () => (Success: true, Message: SuccessMessage),
                    onFailure: error => (Success: false, Message: error)));

        return Complete(trace, outcome.Success, outcome.Message);
    }

    public static ResourceCleanupResult ExecuteLanguageExtPipeline(CleanupScenario scenario)
    {
        var trace = new List<string>();

        var outcome = WithResource(trace, resource =>
            Right<string, Unit>(unit)
                .Map(_ =>
                {
                    resource.Write("begin");
                    return unit;
                })
                .Bind(_ => scenario.ShouldFail
                    ? Left<string, Unit>(FailureMessage)
                    : Right<string, Unit>(unit))
                .Map(_ =>
                {
                    resource.Write("complete");
                    return unit;
                })
                .Match(
                    Right: _ => (Success: true, Message: SuccessMessage),
                    Left: error => (Success: false, Message: error)));

        return Complete(trace, outcome.Success, outcome.Message);
    }

    public static string FormatSummary(ResourceCleanupResult result) =>
        result.Success
            ? $"Success: {result.Message}"
            : $"Failed: {result.Message}";

    public static string FormatTrace(IReadOnlyList<string> trace) =>
        trace.Count == 0
            ? "[none]"
            : string.Join(" -> ", trace);

    private static T WithResource<T>(
        List<string> trace,
        Func<TraceResource, T> useResource)
    {
        TraceResource? resource = null;

        try
        {
            resource = Acquire(trace);
            return useResource(resource);
        }
        finally
        {
            resource?.Dispose();
        }
    }

    private static TraceResource Acquire(List<string> trace)
    {
        trace.Add("acquire: audit-resource");
        return new TraceResource(trace);
    }

    private static ResourceCleanupResult Complete(List<string> trace, bool success, string message) =>
        new(success, message, trace.ToArray());

    public const string SuccessMessage = "Resource work completed.";
    public const string FailureMessage = "simulated resource failure";

    private sealed class TraceResource(List<string> trace) : IDisposable
    {
        public void Write(string entry) => trace.Add($"write: {entry}");

        public void Dispose() => trace.Add("release: audit-resource");
    }

    private readonly record struct StepResult(bool IsSuccess, string? Error)
    {
        public static StepResult Succeed() => new(true, null);
        public static StepResult Fail(string error) => new(false, error);

        public StepResult Bind(Func<StepResult> next) =>
            IsSuccess ? next() : this;

        public StepResult Tap(Action action)
        {
            if (IsSuccess)
            {
                action();
            }

            return this;
        }

        public T Match<T>(Func<T> onSuccess, Func<string, T> onFailure) =>
            IsSuccess ? onSuccess() : onFailure(Error ?? string.Empty);
    }
}
