using LanguageExt;
using static LanguageExt.Prelude;

namespace Scott.FizzBuzz.Core.Demos.RetryBackoffTriad;

public static class RetryBackoffRules
{
    public sealed record RetryPolicy(string Name, int MaxRetries, TimeSpan InitialDelay, decimal Multiplier);

    public sealed record RetryExecutionResult(bool Success, int Attempts, Seq<TimeSpan> BackoffSchedule)
    {
        public int RetriesUsed => Math.Max(0, Attempts - 1);
    }

    private sealed record FoldState(bool Completed, bool Success, int Attempts, Seq<TimeSpan> Schedule);

    public static Either<string, RetryPolicy> ResolvePolicy(string? name) =>
        (name ?? string.Empty).Trim().ToLowerInvariant() switch
        {
            "linear" => Right<string, RetryPolicy>(new RetryPolicy("linear", MaxRetries: 5, InitialDelay: TimeSpan.FromMilliseconds(100), Multiplier: 1m)),
            "exponential" or "exp" or "" => Right<string, RetryPolicy>(new RetryPolicy("exponential", MaxRetries: 5, InitialDelay: TimeSpan.FromMilliseconds(100), Multiplier: 2m)),
            _ => Left<string, RetryPolicy>("Policy must be one of: exponential|exp|linear.")
        };

    public static Either<string, int> ParseFailuresBeforeSuccess(string? number) =>
        int.TryParse(number, out var parsed)
            ? parsed is >= 0 and <= 10
                ? Right<string, int>(parsed)
                : Left<string, int>("Failures-before-success must be between 0 and 10.")
            : Left<string, int>("Failures-before-success must be an integer.");

    public static TimeSpan DelayForAttempt(RetryPolicy policy, int retryAttempt)
    {
        var multiplier = policy.Name == "linear"
            ? retryAttempt
            : Pow(policy.Multiplier, retryAttempt - 1);

        var milliseconds = (double)policy.InitialDelay.TotalMilliseconds * (double)multiplier;
        return TimeSpan.FromMilliseconds(milliseconds);
    }

    public static RetryExecutionResult ExecuteImperative(RetryPolicy policy, int failuresBeforeSuccess)
    {
        var attempt = 0;
        var schedule = Seq<TimeSpan>();

        while (attempt <= policy.MaxRetries)
        {
            attempt++;
            var shouldFail = attempt <= failuresBeforeSuccess;

            if (!shouldFail)
            {
                return new RetryExecutionResult(Success: true, Attempts: attempt, BackoffSchedule: schedule);
            }

            if (attempt <= policy.MaxRetries)
            {
                schedule = schedule.Add(DelayForAttempt(policy, attempt));
            }
        }

        return new RetryExecutionResult(Success: false, Attempts: policy.MaxRetries + 1, BackoffSchedule: schedule);
    }

    public static RetryExecutionResult ExecuteCSharpPipeline(RetryPolicy policy, int failuresBeforeSuccess)
    {
        var initial = new FoldState(Completed: false, Success: false, Attempts: 0, Schedule: Seq<TimeSpan>());

        var final = Enumerable.Range(1, policy.MaxRetries + 1)
            .Aggregate(initial, (state, attempt) =>
            {
                if (state.Completed)
                {
                    return state;
                }

                if (attempt > failuresBeforeSuccess)
                {
                    return state with { Completed = true, Success = true, Attempts = attempt };
                }

                var nextSchedule = attempt <= policy.MaxRetries
                    ? state.Schedule.Add(DelayForAttempt(policy, attempt))
                    : state.Schedule;

                var exhausted = attempt == policy.MaxRetries + 1;
                return state with
                {
                    Completed = exhausted,
                    Success = false,
                    Attempts = attempt,
                    Schedule = nextSchedule
                };
            });

        return new RetryExecutionResult(final.Success, final.Attempts, final.Schedule);
    }

    public static RetryExecutionResult ExecuteLanguageExtPipeline(RetryPolicy policy, int failuresBeforeSuccess)
    {
        var initial = new FoldState(Completed: false, Success: false, Attempts: 0, Schedule: Seq<TimeSpan>());

        var final = Range(1, policy.MaxRetries + 1)
            .Fold(initial, (state, attempt) =>
            {
                if (state.Completed)
                {
                    return state;
                }

                if (attempt > failuresBeforeSuccess)
                {
                    return state with { Completed = true, Success = true, Attempts = attempt };
                }

                var nextSchedule = attempt <= policy.MaxRetries
                    ? state.Schedule.Add(DelayForAttempt(policy, attempt))
                    : state.Schedule;

                var exhausted = attempt == policy.MaxRetries + 1;
                return state with
                {
                    Completed = exhausted,
                    Success = false,
                    Attempts = attempt,
                    Schedule = nextSchedule
                };
            });

        return new RetryExecutionResult(final.Success, final.Attempts, final.Schedule);
    }

    public static string FormatSchedule(Seq<TimeSpan> schedule) =>
        schedule.Count == 0
            ? "[none]"
            : string.Join(", ", schedule.Select(delay => $"{delay.TotalMilliseconds:0}ms"));

    public static string FormatSummary(RetryExecutionResult result) =>
        result.Success
            ? $"Succeeded after {result.Attempts} attempt(s) with {result.RetriesUsed} retries."
            : $"Failed after {result.Attempts} attempt(s): retries exhausted.";

    private static decimal Pow(decimal baseValue, int exponent)
    {
        var value = 1m;
        for (var i = 0; i < exponent; i++)
        {
            value *= baseValue;
        }

        return value;
    }
}
