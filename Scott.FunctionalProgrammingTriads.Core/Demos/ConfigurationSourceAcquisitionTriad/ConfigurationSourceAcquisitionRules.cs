using LanguageExt;
using Scott.FunctionalProgrammingTriads.Core.Demos.ConfigurationLoadingTriad;
using static LanguageExt.Prelude;

namespace Scott.FunctionalProgrammingTriads.Core.Demos.ConfigurationSourceAcquisitionTriad;

public static class ConfigurationSourceAcquisitionRules
{
    public sealed record ExternalSettingSource(IReadOnlyDictionary<string, string> Values);

    public sealed record StartupSourceAcquisitionResult(ConfigurationLoadingRules.StartupSettingSource? Source, IReadOnlyList<string> Errors)
    {
        public bool IsSuccess => Source is not null && Errors.Count == 0;

        public static StartupSourceAcquisitionResult Success(ConfigurationLoadingRules.StartupSettingSource source) => new(source, System.Array.Empty<string>());
        public static StartupSourceAcquisitionResult Fail(string error) => new(null, new[] { error });
        public static StartupSourceAcquisitionResult Fail(IReadOnlyList<string> errors) => new(null, errors);
    }

    public static bool TryResolveScenario(string? name, out ExternalSettingSource? source, out string? error)
    {
        switch ((name ?? string.Empty).Trim().ToLowerInvariant())
        {
            case "":
            case "success":
            case "prod":
                source = new ExternalSettingSource(new Dictionary<string, string>
                {
                    ["TRIADS_APP_NAME"] = " triad-service ",
                    ["TRIADS_ENVIRONMENT"] = " production ",
                    ["TRIADS_DATABASE_URL"] = " postgres://db.example.com/triads ",
                    ["TRIADS_PORT"] = " 8080 ",
                    ["TRIADS_LOG_LEVEL"] = " warn "
                });
                error = null;
                return true;
            case "alias":
            case "dev":
                source = new ExternalSettingSource(new Dictionary<string, string>
                {
                    ["TRIADS_APP_NAME"] = " worker-service ",
                    ["TRIADS_ENVIRONMENT"] = " dev ",
                    ["TRIADS_DB_URL"] = " postgres://localhost/worker ",
                    ["TRIADS_APP_PORT"] = " 9090 ",
                    ["TRIADS_LOG_LEVEL"] = " info "
                });
                error = null;
                return true;
            case "missing":
                source = new ExternalSettingSource(new Dictionary<string, string>
                {
                    ["TRIADS_APP_NAME"] = " ",
                    ["TRIADS_ENVIRONMENT"] = " staging ",
                    ["TRIADS_APP_PORT"] = " 7000 "
                });
                error = null;
                return true;
            default:
                source = null;
                error = "Scenario must be one of: success|prod|alias|dev|missing.";
                return false;
        }
    }

    public static StartupSourceAcquisitionResult ExecuteImperative(string? name)
    {
        if (!TryResolveScenario(name, out var source, out var error))
            return StartupSourceAcquisitionResult.Fail(error ?? "Unknown configuration-source scenario.");

        return AcquireImperative(source!);
    }

    public static StartupSourceAcquisitionResult ExecuteCSharpPipeline(string? name)
    {
        if (!TryResolveScenario(name, out var source, out var error))
            return StartupSourceAcquisitionResult.Fail(error ?? "Unknown configuration-source scenario.");

        return AcquireCSharp(source!);
    }

    public static Either<string, ConfigurationLoadingRules.StartupSettingSource> ExecuteLanguageExtPipeline(string? name)
    {
        if (!TryResolveScenario(name, out var source, out var error))
            return Left<string, ConfigurationLoadingRules.StartupSettingSource>(error ?? "Unknown configuration-source scenario.");

        return AcquireLanguageExt(source!)
            .ToEither()
            .MapLeft(errors => string.Join(" ", errors));
    }

    public static StartupSourceAcquisitionResult AcquireImperative(ExternalSettingSource source)
    {
        if (!TryReadRequired(source.Values, "TRIADS_APP_NAME", out var appName, out var error))
            return StartupSourceAcquisitionResult.Fail(error!);
        if (!TryReadRequired(source.Values, "TRIADS_ENVIRONMENT", out var environmentName, out error))
            return StartupSourceAcquisitionResult.Fail(error!);
        if (!TryReadRequired(source.Values, "TRIADS_DATABASE_URL", "TRIADS_DB_URL", out var databaseUrl, out error))
            return StartupSourceAcquisitionResult.Fail(error!);
        if (!TryReadRequired(source.Values, "TRIADS_PORT", "TRIADS_APP_PORT", out var portText, out error))
            return StartupSourceAcquisitionResult.Fail(error!);
        if (!TryReadRequired(source.Values, "TRIADS_LOG_LEVEL", out var logLevel, out error))
            return StartupSourceAcquisitionResult.Fail(error!);

        return StartupSourceAcquisitionResult.Success(BuildStartupSettingSource(appName!, environmentName!, databaseUrl!, portText!, logLevel!));
    }

    public static StartupSourceAcquisitionResult AcquireCSharp(ExternalSettingSource source)
    {
        var errors = new List<string>();
        var appName = ReadRequired(source.Values, errors, "TRIADS_APP_NAME");
        var environmentName = ReadRequired(source.Values, errors, "TRIADS_ENVIRONMENT");
        var databaseUrl = ReadRequired(source.Values, errors, "TRIADS_DATABASE_URL", "TRIADS_DB_URL");
        var portText = ReadRequired(source.Values, errors, "TRIADS_PORT", "TRIADS_APP_PORT");
        var logLevel = ReadRequired(source.Values, errors, "TRIADS_LOG_LEVEL");

        return errors.Count == 0
            ? StartupSourceAcquisitionResult.Success(BuildStartupSettingSource(appName!, environmentName!, databaseUrl!, portText!, logLevel!))
            : StartupSourceAcquisitionResult.Fail(errors);
    }

    public static Validation<Seq<string>, ConfigurationLoadingRules.StartupSettingSource> AcquireLanguageExt(ExternalSettingSource source) =>
        (
            LoadRequired(source.Values, "TRIADS_APP_NAME"),
            LoadRequired(source.Values, "TRIADS_ENVIRONMENT"),
            LoadRequired(source.Values, "TRIADS_DATABASE_URL", "TRIADS_DB_URL"),
            LoadRequired(source.Values, "TRIADS_PORT", "TRIADS_APP_PORT"),
            LoadRequired(source.Values, "TRIADS_LOG_LEVEL")
        )
        .Apply((appName, environmentName, databaseUrl, portText, logLevel) =>
            BuildStartupSettingSource(appName, environmentName, databaseUrl, portText, logLevel));

    public static string FormatSummary(ConfigurationLoadingRules.StartupSettingSource source) =>
        string.Join(", ",
            source.Values
                .OrderBy(static pair => pair.Key)
                .Select(static pair => $"{pair.Key}={pair.Value}"));

    private static ConfigurationLoadingRules.StartupSettingSource BuildStartupSettingSource(
        string appName,
        string environmentName,
        string databaseUrl,
        string portText,
        string logLevel) =>
        new(new Dictionary<string, string>
        {
            ["APP_NAME"] = appName.Trim(),
            ["ENVIRONMENT"] = environmentName.Trim(),
            ["DATABASE_URL"] = databaseUrl.Trim(),
            ["PORT"] = portText.Trim(),
            ["LOG_LEVEL"] = logLevel.Trim()
        });

    private static bool TryReadRequired(IReadOnlyDictionary<string, string> values, string primaryKey, out string? value, out string? error) =>
        TryReadRequired(values, primaryKey, alternateKey: null, out value, out error);

    private static bool TryReadRequired(IReadOnlyDictionary<string, string> values, string primaryKey, string? alternateKey, out string? value, out string? error)
    {
        var candidate = FindValue(values, primaryKey, alternateKey);
        if (string.IsNullOrWhiteSpace(candidate))
        {
            value = null;
            error = $"{primaryKey} is required from the external source.";
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
            errors.Add($"{primaryKey} is required from the external source.");
            return null;
        }

        return candidate;
    }

    private static Validation<Seq<string>, string> LoadRequired(IReadOnlyDictionary<string, string> values, string primaryKey, string? alternateKey = null)
    {
        var candidate = FindValue(values, primaryKey, alternateKey);
        return string.IsNullOrWhiteSpace(candidate)
            ? Fail<Seq<string>, string>(Seq1($"{primaryKey} is required from the external source."))
            : Success<Seq<string>, string>(candidate!);
    }

    private static string? FindValue(IReadOnlyDictionary<string, string> values, string primaryKey, string? alternateKey)
    {
        if (values.TryGetValue(primaryKey, out var primary))
            return primary;
        if (alternateKey is not null && values.TryGetValue(alternateKey, out var alternate))
            return alternate;
        return null;
    }
}
