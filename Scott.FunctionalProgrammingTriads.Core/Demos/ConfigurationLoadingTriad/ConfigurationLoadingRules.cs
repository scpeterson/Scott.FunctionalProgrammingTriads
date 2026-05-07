using LanguageExt;
using static LanguageExt.Prelude;

namespace Scott.FunctionalProgrammingTriads.Core.Demos.ConfigurationLoadingTriad;

public static class ConfigurationLoadingRules
{
    public sealed record StartupSettingSource(IReadOnlyDictionary<string, string> Values);

    public sealed record RawStartupConfig(string AppName, string EnvironmentName, string DatabaseUrl, string PortText, string LogLevel);

    public sealed record StartupLoadingResult(RawStartupConfig? Config, IReadOnlyList<string> Errors)
    {
        public bool IsSuccess => Config is not null && Errors.Count == 0;

        public static StartupLoadingResult Success(RawStartupConfig config) => new(config, System.Array.Empty<string>());
        public static StartupLoadingResult Fail(string error) => new(null, new[] { error });
        public static StartupLoadingResult Fail(IReadOnlyList<string> errors) => new(null, errors);
    }

    public static bool TryResolveScenario(string? name, out StartupSettingSource? source, out string? error)
    {
        switch ((name ?? string.Empty).Trim().ToLowerInvariant())
        {
            case "":
            case "success":
            case "prod":
                source = BuildSource(new Dictionary<string, string>
                {
                    ["APP_NAME"] = "  triad-service  ",
                    ["ENVIRONMENT"] = "  Production  ",
                    ["DB_URL"] = "  postgres://db.example.com/triads  ",
                    ["APP_PORT"] = " 8080 ",
                    ["LOG_LEVEL"] = " WARN "
                });
                error = null;
                return true;
            case "missing":
                source = BuildSource(new Dictionary<string, string>
                {
                    ["APP_NAME"] = "  ",
                    ["ENVIRONMENT"] = " staging ",
                    ["DB_URL"] = "",
                    ["APP_PORT"] = "7000"
                });
                error = null;
                return true;
            case "mixed":
            case "broken":
                source = BuildSource(new Dictionary<string, string>
                {
                    ["APP_NAME"] = " worker-service ",
                    ["ENVIRONMENT"] = " DEV ",
                    ["DATABASE_URL"] = " postgres://localhost/worker ",
                    ["PORT"] = " 9090 ",
                    ["LOG_LEVEL"] = " INFO "
                });
                error = null;
                return true;
            default:
                source = null;
                error = "Scenario must be one of: success|prod|missing|broken|mixed.";
                return false;
        }
    }

    public static StartupLoadingResult ExecuteImperative(string? name)
    {
        if (!TryResolveScenario(name, out var source, out var error))
        {
            return StartupLoadingResult.Fail(error ?? "Unknown configuration-loading scenario.");
        }

        return LoadImperative(source!);
    }

    public static StartupLoadingResult ExecuteCSharpPipeline(string? name)
    {
        if (!TryResolveScenario(name, out var source, out var error))
        {
            return StartupLoadingResult.Fail(error ?? "Unknown configuration-loading scenario.");
        }

        return LoadCSharp(source!);
    }

    public static Either<string, RawStartupConfig> ExecuteLanguageExtPipeline(string? name)
    {
        if (!TryResolveScenario(name, out var source, out var error))
        {
            return Left<string, RawStartupConfig>(error ?? "Unknown configuration-loading scenario.");
        }

        return LoadLanguageExt(source!)
            .ToEither()
            .MapLeft(errors => string.Join(" ", errors));
    }

    public static StartupLoadingResult LoadImperative(StartupSettingSource source)
    {
        if (!TryReadRequired(source.Values, "APP_NAME", out var appName, out var error))
        {
            return StartupLoadingResult.Fail(error!);
        }

        if (!TryReadRequired(source.Values, "ENVIRONMENT", out var environment, out error))
        {
            return StartupLoadingResult.Fail(error!);
        }

        if (!TryReadRequired(source.Values, "DATABASE_URL", "DB_URL", out var databaseUrl, out error))
        {
            return StartupLoadingResult.Fail(error!);
        }

        if (!TryReadRequired(source.Values, "PORT", "APP_PORT", out var portText, out error))
        {
            return StartupLoadingResult.Fail(error!);
        }

        if (!TryReadRequired(source.Values, "LOG_LEVEL", out var logLevel, out error))
        {
            return StartupLoadingResult.Fail(error!);
        }

        return StartupLoadingResult.Success(Normalize(appName!, environment!, databaseUrl!, portText!, logLevel!));
    }

    public static StartupLoadingResult LoadCSharp(StartupSettingSource source)
    {
        var errors = new List<string>();
        var appName = ReadRequired(source.Values, errors, "APP_NAME");
        var environment = ReadRequired(source.Values, errors, "ENVIRONMENT");
        var databaseUrl = ReadRequired(source.Values, errors, "DATABASE_URL", "DB_URL");
        var portText = ReadRequired(source.Values, errors, "PORT", "APP_PORT");
        var logLevel = ReadRequired(source.Values, errors, "LOG_LEVEL");

        return errors.Count == 0
            ? StartupLoadingResult.Success(Normalize(appName!, environment!, databaseUrl!, portText!, logLevel!))
            : StartupLoadingResult.Fail(errors);
    }

    public static Validation<Seq<string>, RawStartupConfig> LoadLanguageExt(StartupSettingSource source) =>
        (
            LoadRequired(source.Values, "APP_NAME"),
            LoadRequired(source.Values, "ENVIRONMENT"),
            LoadRequired(source.Values, "DATABASE_URL", "DB_URL"),
            LoadRequired(source.Values, "PORT", "APP_PORT"),
            LoadRequired(source.Values, "LOG_LEVEL")
        )
        .Apply((appName, environment, databaseUrl, portText, logLevel) =>
            Normalize(appName, environment, databaseUrl, portText, logLevel));

    public static string FormatSummary(RawStartupConfig config) =>
        $"App={config.AppName}, Env={config.EnvironmentName}, Db={config.DatabaseUrl}, Port={config.PortText}, LogLevel={config.LogLevel}";

    private static StartupSettingSource BuildSource(Dictionary<string, string> values) => new(values);

    private static RawStartupConfig Normalize(string appName, string environment, string databaseUrl, string portText, string logLevel) =>
        new(
            appName.Trim(),
            environment.Trim().ToLowerInvariant(),
            databaseUrl.Trim(),
            portText.Trim(),
            logLevel.Trim().ToLowerInvariant());

    private static bool TryReadRequired(
        IReadOnlyDictionary<string, string> values,
        string primaryKey,
        out string? value,
        out string? error) =>
        TryReadRequired(values, primaryKey, alternateKey: null, out value, out error);

    private static bool TryReadRequired(
        IReadOnlyDictionary<string, string> values,
        string primaryKey,
        string? alternateKey,
        out string? value,
        out string? error)
    {
        var candidate = FindValue(values, primaryKey, alternateKey);
        if (string.IsNullOrWhiteSpace(candidate))
        {
            value = null;
            error = $"{primaryKey} is required.";
            return false;
        }

        value = candidate;
        error = null;
        return true;
    }

    private static string? ReadRequired(IReadOnlyDictionary<string, string> values, List<string> errors, string primaryKey, string? alternateKey = null)
    {
        var candidate = FindValue(values, primaryKey, alternateKey);
        if (string.IsNullOrWhiteSpace(candidate))
        {
            errors.Add($"{primaryKey} is required.");
            return null;
        }

        return candidate;
    }

    private static Validation<Seq<string>, string> LoadRequired(IReadOnlyDictionary<string, string> values, string primaryKey, string? alternateKey = null)
    {
        var candidate = FindValue(values, primaryKey, alternateKey);
        return string.IsNullOrWhiteSpace(candidate)
            ? Fail<Seq<string>, string>(Seq1($"{primaryKey} is required."))
            : Success<Seq<string>, string>(candidate!);
    }

    private static string? FindValue(IReadOnlyDictionary<string, string> values, string primaryKey, string? alternateKey)
    {
        if (values.TryGetValue(primaryKey, out var primary))
        {
            return primary;
        }

        if (alternateKey is not null && values.TryGetValue(alternateKey, out var alternate))
        {
            return alternate;
        }

        return null;
    }
}
