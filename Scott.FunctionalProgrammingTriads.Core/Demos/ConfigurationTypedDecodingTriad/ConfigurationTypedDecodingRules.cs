using LanguageExt;
using Scott.FunctionalProgrammingTriads.Core.Demos.ConfigurationLoadingTriad;
using static LanguageExt.Prelude;

namespace Scott.FunctionalProgrammingTriads.Core.Demos.ConfigurationTypedDecodingTriad;

public static class ConfigurationTypedDecodingRules
{
    public enum StartupEnvironmentName
    {
        Development,
        Staging,
        Production
    }

    public enum StartupLogLevel
    {
        Debug,
        Info,
        Warn,
        Error
    }

    public sealed record DecodedStartupConfig(
        string AppName,
        StartupEnvironmentName Environment,
        string DatabaseUrl,
        int Port,
        StartupLogLevel LogLevel);

    public sealed record StartupDecodingResult(DecodedStartupConfig? Config, IReadOnlyList<string> Errors)
    {
        public bool IsSuccess => Config is not null && Errors.Count == 0;

        public static StartupDecodingResult Success(DecodedStartupConfig config) => new(config, System.Array.Empty<string>());
        public static StartupDecodingResult Fail(string error) => new(null, new[] { error });
        public static StartupDecodingResult Fail(IReadOnlyList<string> errors) => new(null, errors);
    }

    public static bool TryResolveScenario(string? name, out ConfigurationLoadingRules.RawStartupConfig? config, out string? error)
    {
        switch ((name ?? string.Empty).Trim().ToLowerInvariant())
        {
            case "":
            case "success":
            case "prod":
                config = new ConfigurationLoadingRules.RawStartupConfig(
                    "triad-service",
                    "production",
                    "postgres://db.example.com/triads",
                    "8080",
                    "warn");
                error = null;
                return true;
            case "alias":
            case "dev":
                config = new ConfigurationLoadingRules.RawStartupConfig(
                    "worker-service",
                    "dev",
                    "postgres://localhost/worker",
                    "9090",
                    "info");
                error = null;
                return true;
            case "invalid":
            case "broken":
                config = new ConfigurationLoadingRules.RawStartupConfig(
                    "triad-service",
                    "qa",
                    "postgres://db.example.com/triads",
                    "eighty",
                    "loud");
                error = null;
                return true;
            case "range":
                config = new ConfigurationLoadingRules.RawStartupConfig(
                    "triad-service",
                    "staging",
                    "postgres://db.example.com/triads",
                    "70000",
                    "debug");
                error = null;
                return true;
            default:
                config = null;
                error = "Scenario must be one of: success|prod|alias|dev|invalid|broken|range.";
                return false;
        }
    }

    public static StartupDecodingResult ExecuteImperative(string? name)
    {
        if (!TryResolveScenario(name, out var config, out var error))
            return StartupDecodingResult.Fail(error ?? "Unknown configuration-decoding scenario.");

        return DecodeImperative(config!);
    }

    public static StartupDecodingResult ExecuteCSharpPipeline(string? name)
    {
        if (!TryResolveScenario(name, out var config, out var error))
            return StartupDecodingResult.Fail(error ?? "Unknown configuration-decoding scenario.");

        return DecodeCSharp(config!);
    }

    public static Either<string, DecodedStartupConfig> ExecuteLanguageExtPipeline(string? name)
    {
        if (!TryResolveScenario(name, out var config, out var error))
            return Left<string, DecodedStartupConfig>(error ?? "Unknown configuration-decoding scenario.");

        return DecodeLanguageExt(config!)
            .ToEither()
            .MapLeft(errors => string.Join(" ", errors));
    }

    public static StartupDecodingResult DecodeImperative(ConfigurationLoadingRules.RawStartupConfig rawConfig)
    {
        if (!TryDecodeEnvironment(rawConfig.EnvironmentName, out var environment, out var error))
            return StartupDecodingResult.Fail(error!);

        if (!TryDecodePort(rawConfig.PortText, out var port, out error))
            return StartupDecodingResult.Fail(error!);

        if (!TryDecodeLogLevel(rawConfig.LogLevel, out var logLevel, out error))
            return StartupDecodingResult.Fail(error!);

        return StartupDecodingResult.Success(new DecodedStartupConfig(rawConfig.AppName, environment, rawConfig.DatabaseUrl, port, logLevel));
    }

    public static StartupDecodingResult DecodeCSharp(ConfigurationLoadingRules.RawStartupConfig rawConfig)
    {
        var errors = new List<string>();
        var hasEnvironment = TryDecodeEnvironment(rawConfig.EnvironmentName, out var environment, out var environmentError);
        var hasPort = TryDecodePort(rawConfig.PortText, out var port, out var portError);
        var hasLogLevel = TryDecodeLogLevel(rawConfig.LogLevel, out var logLevel, out var logLevelError);

        AddError(environmentError, errors);
        AddError(portError, errors);
        AddError(logLevelError, errors);

        return errors.Count == 0
            ? StartupDecodingResult.Success(new DecodedStartupConfig(rawConfig.AppName, environment, rawConfig.DatabaseUrl, port, logLevel))
            : StartupDecodingResult.Fail(errors);
    }

    public static Validation<Seq<string>, DecodedStartupConfig> DecodeLanguageExt(ConfigurationLoadingRules.RawStartupConfig rawConfig) =>
        (
            DecodeEnvironment(rawConfig.EnvironmentName),
            DecodePort(rawConfig.PortText),
            DecodeLogLevel(rawConfig.LogLevel)
        )
        .Apply((environment, port, logLevel) =>
            new DecodedStartupConfig(rawConfig.AppName, environment, rawConfig.DatabaseUrl, port, logLevel));

    public static string FormatSummary(DecodedStartupConfig config) =>
        $"App={config.AppName}, Env={config.Environment}, Db={config.DatabaseUrl}, Port={config.Port}, LogLevel={config.LogLevel}";

    private static void AddError(string? error, List<string> errors)
    {
        if (!string.IsNullOrWhiteSpace(error))
            errors.Add(error);
    }

    private static bool TryDecodeEnvironment(string environmentName, out StartupEnvironmentName environment, out string? error)
    {
        switch ((environmentName ?? string.Empty).Trim().ToLowerInvariant())
        {
            case "dev":
            case "development":
                environment = StartupEnvironmentName.Development;
                error = null;
                return true;
            case "staging":
                environment = StartupEnvironmentName.Staging;
                error = null;
                return true;
            case "prod":
            case "production":
                environment = StartupEnvironmentName.Production;
                error = null;
                return true;
            default:
                environment = default;
                error = "Environment must be development, staging, or production.";
                return false;
        }
    }

    private static bool TryDecodePort(string portText, out int port, out string? error)
    {
        if (!int.TryParse(portText, out port))
        {
            error = "Port must be an integer.";
            return false;
        }

        if (port is < 1024 or > 65535)
        {
            error = "Port must be between 1024 and 65535.";
            return false;
        }

        error = null;
        return true;
    }

    private static bool TryDecodeLogLevel(string logLevelText, out StartupLogLevel logLevel, out string? error)
    {
        switch ((logLevelText ?? string.Empty).Trim().ToLowerInvariant())
        {
            case "debug":
                logLevel = StartupLogLevel.Debug;
                error = null;
                return true;
            case "info":
                logLevel = StartupLogLevel.Info;
                error = null;
                return true;
            case "warn":
                logLevel = StartupLogLevel.Warn;
                error = null;
                return true;
            case "error":
                logLevel = StartupLogLevel.Error;
                error = null;
                return true;
            default:
                logLevel = default;
                error = "Log level must be debug, info, warn, or error.";
                return false;
        }
    }

    private static Validation<Seq<string>, StartupEnvironmentName> DecodeEnvironment(string environmentName) =>
        TryDecodeEnvironment(environmentName, out var environment, out var error)
            ? Success<Seq<string>, StartupEnvironmentName>(environment)
            : Fail<Seq<string>, StartupEnvironmentName>(Seq1(error!));

    private static Validation<Seq<string>, int> DecodePort(string portText) =>
        TryDecodePort(portText, out var port, out var error)
            ? Success<Seq<string>, int>(port)
            : Fail<Seq<string>, int>(Seq1(error!));

    private static Validation<Seq<string>, StartupLogLevel> DecodeLogLevel(string logLevelText) =>
        TryDecodeLogLevel(logLevelText, out var logLevel, out var error)
            ? Success<Seq<string>, StartupLogLevel>(logLevel)
            : Fail<Seq<string>, StartupLogLevel>(Seq1(error!));
}
