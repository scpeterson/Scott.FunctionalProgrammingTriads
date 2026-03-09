using LanguageExt;
using Scott.FizzBuzz.Core.Interfaces;
using static Scott.FizzBuzz.Core.OutputUtilities;

namespace Scott.FizzBuzz.Core.Demos.ValidationMonadTriad;

public class CSharpValidationMonadComparisonDemo : IDemo
{
    private readonly IOutput _output;

    public CSharpValidationMonadComparisonDemo() : this(new ConsoleOutput())
    {
    }

    public CSharpValidationMonadComparisonDemo(IOutput output)
    {
        _output = output;
    }

    public string Key => "csharp-validation-monad-comparison";
    public string Category => "csharp";
    public IReadOnlyCollection<string> Tags => ["fp", "csharp", "comparison", "validation", "monad"];
    public string Description => "Manual error-list accumulation in C# to emulate Validation-style accumulation.";

    public Either<string, Unit> Run(string? name, string? number) =>
        ExecuteWithSpacing(_output, () =>
        {
            var errors = new List<string>();
            var normalizedName = name?.Trim() ?? string.Empty;

            if (normalizedName.Length == 0)
            {
                errors.Add("Name is required.");
            }

            if (normalizedName.Length < 3)
            {
                errors.Add("Name must be at least 3 characters.");
            }

            if (normalizedName.Any(ch => !char.IsLetter(ch)))
            {
                errors.Add("Name must contain letters only.");
            }

            if (!int.TryParse(number, out var age))
            {
                errors.Add("Age must be numeric.");
            }
            else if (age is < 18 or > 120)
            {
                errors.Add("Age must be between 18 and 120.");
            }

            if (errors.Count > 0)
            {
                _output.WriteLine(string.Join(" | ", errors));
                _output.WriteLine("C#/.NET comparison note: explicit List<string> accumulation is required.");
                return;
            }

            _output.WriteLine($"Validated candidate: {normalizedName} ({age})");
            _output.WriteLine("C#/.NET comparison note: custom accumulation logic still required to gather all errors.");
        }, "C# Validation Monad Comparison");
}
