using AutoFixture.Xunit2;
using FluentAssertions;
using Scott.FunctionalProgrammingTriads.Core.Demos.DotNet10Features;

namespace Scott.FunctionalProgrammingTriads.Console.Tests;

public class DemoRunnerShould
{
    [Fact]
    public void ThrowArgumentExceptionWhenDemoKeysAreDuplicated()
    {
        // Arrange / Act
        Action ctor = () => _ = new DemoRunner([
            new StubDemo("duplicate-key", (_, _) => DemoExecutionResult.Success()),
            new StubDemo("duplicate-key", (_, _) => DemoExecutionResult.Success())
        ]);

        // Assert
        ctor.Should().Throw<ArgumentException>()
            .WithMessage("*Duplicate demo key(s): duplicate-key*");
    }

    [Fact]
    public void ThrowArgumentNullExceptionWhenInputIsNull()
    {
        // Arrange / Act
        Action ctor = () => _ = new DemoRunner(null!);

        // Assert
        ctor.Should().Throw<ArgumentNullException>()
            .WithParameterName("demos");
    }
    
    [Fact]
    public void ReturnLeftWhenOptsIsNull()
    {
        // Arrange: runner with an empty list of demos
        var runner = new DemoRunner([]);

        // Act
        var result = runner.Execute(null!);

        // Assert
        result.ShouldBeLeft(msg => msg.Should().Be("opts was null"));
    }
    
    [Fact]
    public void ReturnLeftWhenNoMethodIsSpecified()
    {
        // Arrange: runner with no demos (so lookup doesn’t matter)
        var runner = new DemoRunner([]);
        var opts = new Options { Method = string.Empty, Name = null, Number = null };

        // Act
        var result = runner.Execute(opts);

        // Assert
        result.ShouldBeLeft(msg => msg.Should().Be("No method specified"));
    }

    [Fact]
    public void ReturnLeftWhenFirstHourIsUsedWithoutList()
    {
        // Arrange
        var runner = new DemoRunner([]);
        var opts = new Options { Method = "any", FirstHour = true };

        // Act
        var result = runner.Execute(opts);

        // Assert
        result.ShouldBeLeft(msg => msg.Should().Be("--first-hour can only be used with --list."));
    }

    [Fact]
    public void ReturnLeftWhenTagIsUsedWithoutList()
    {
        // Arrange
        var runner = new DemoRunner([]);
        var opts = new Options { Method = "any", Tags = ["fp"] };

        // Act
        var result = runner.Execute(opts);

        // Assert
        result.ShouldBeLeft(msg => msg.Should().Be("--tag can only be used with --list."));
    }

    [Fact]
    public void ReturnLeftWhenMethodIsUsedWithList()
    {
        // Arrange
        var runner = new DemoRunner([]);
        var opts = new Options { List = true, Method = "imperative" };

        // Act
        var result = runner.Execute(opts);

        // Assert
        result.ShouldBeLeft(msg => msg.Should().Be("--method cannot be combined with --list."));
    }

    [Fact]
    public void ReturnLeftWhenNameOrNumberIsUsedWithList()
    {
        // Arrange
        var runner = new DemoRunner([]);
        var opts = new Options { List = true, Name = "scott", Number = "12" };

        // Act
        var result = runner.Execute(opts);

        // Assert
        result.ShouldBeLeft(msg => msg.Should().Be("--name/--number cannot be combined with --list."));
    }

    [Fact]
    public void ListDemosWithoutTagFilters()
    {
        // Arrange
        var fpDemo = new StubDemo(
            "fp-demo",
            (_, _) => DemoExecutionResult.Success(),
            category: "csharp",
            tags: ["comparison", "fp"]);
        var imperativeDemo = new StubDemo(
            "imperative-demo",
            (_, _) => DemoExecutionResult.Success(),
            category: "imperative",
            tags: ["baseline"]);
        var output = new RecordingOutputSink();
        var runner = new DemoRunner([fpDemo, imperativeDemo], output);
        var opts = new Options { List = true };

        // Act
        var result = runner.Execute(opts);

        // Assert
        result.ShouldBeRight();
        output.Lines.Should().Contain("== Triad Comparisons ==");
        output.Lines.Should().Contain("== Core Baseline Demos ==");
        output.Lines.Should().Contain("fp-demo");
        output.Lines.Should().Contain("imperative-demo");
        output.Lines.Should().Contain("  category: csharp");
        output.Lines.Should().Contain("  category: imperative");
        output.Lines.Should().Contain("  tags: comparison,fp");
        output.Lines.Should().Contain("  tags: baseline");
    }

    [Fact]
    public void ListDemosIncludeDescriptionWhenProvided()
    {
        // Arrange
        var describedDemo = new StubDemo(
            "described-demo",
            (_, _) => DemoExecutionResult.Success(),
            description: "Demonstrates a described listing entry.");
        var output = new RecordingOutputSink();
        var runner = new DemoRunner([describedDemo], output);
        var opts = new Options { List = true };

        // Act
        var result = runner.Execute(opts);

        // Assert
        result.ShouldBeRight();
        output.Lines.Should().Contain("  description: Demonstrates a described listing entry.");
    }

    [Fact]
    public void ListDemosWithTagFilters()
    {
        // Arrange
        var fpDemo = new StubDemo(
            "fp-demo",
            (_, _) => DemoExecutionResult.Success(),
            category: "functional",
            tags: ["fp", "dotnet10"]);
        var imperativeDemo = new StubDemo(
            "imperative-demo",
            (_, _) => DemoExecutionResult.Success(),
            category: "imperative",
            tags: ["imperative"]);
        var output = new RecordingOutputSink();
        var runner = new DemoRunner([fpDemo, imperativeDemo], output);
        var opts = new Options { List = true, Tags = ["fp"] };

        // Act
        var result = runner.Execute(opts);

        // Assert
        result.ShouldBeRight();
        output.Lines.Should().Contain("fp-demo");
        output.Lines.Should().NotContain("imperative-demo");
    }

    [Fact]
    public void ListOnlyFirstHourPathInCuratedOrder()
    {
        // Arrange
        var demos = new[]
        {
            new StubDemo("langext-option-monad-comparison", (_, _) => DemoExecutionResult.Success(), description: "Option demo"),
            new StubDemo("csharp-null-patterns", (_, _) => DemoExecutionResult.Success(), description: "Null demo"),
            new StubDemo("demo-currying", (_, _) => DemoExecutionResult.Success(), description: "Currying demo", tags: ["baseline", "csharp"]),
            new StubDemo("imperative", (_, _) => DemoExecutionResult.Success(), category: "imperative", description: "Imperative demo", tags: ["baseline"]),
            new StubDemo("tuple-demo", (_, _) => DemoExecutionResult.Success(), category: "csharp-support", description: "Tuple demo", tags: ["baseline", "supporting-feature"]),
            new StubDemo("pattern-matching", (_, _) => DemoExecutionResult.Success(), category: "csharp-support", description: "Pattern demo", tags: ["baseline", "supporting-feature"]),
            new StubDemo("csharp-validation-error-list", (_, _) => DemoExecutionResult.Success(), description: "Validation demo"),
            new StubDemo("csharp-parse-validate-pipeline", (_, _) => DemoExecutionResult.Success(), description: "Parse demo"),
            new StubDemo("other-demo", (_, _) => DemoExecutionResult.Success(), description: "Should not appear")
        };
        var output = new RecordingOutputSink();
        var runner = new DemoRunner(demos, output);

        // Act
        var result = runner.Execute(new Options { List = true, FirstHour = true });

        // Assert
        result.ShouldBeRight();
        output.Lines.Should().Contain("== First Hour Path ==");
        output.Lines.Should().ContainInOrder(
            "1. pattern-matching",
            "2. tuple-demo",
            "3. imperative",
            "4. demo-currying",
            "5. csharp-parse-validate-pipeline",
            "6. csharp-null-patterns",
            "7. csharp-validation-error-list",
            "8. langext-option-monad-comparison");
        output.Lines.Should().NotContain("other-demo");
    }

    [Fact]
    public void ListDemosWithDotNet10TagShowsOnlyDotNet10Demos()
    {
        // Arrange
        var dotnet10JsonDemo = new FpJsonStrictValidationDemo();
        var dotnet10ExtensionDemo = new FpExtensionMembersTypeclassesDemo();
        var nonDotNet10Demo = new StubDemo(
            "legacy-demo",
            (_, _) => DemoExecutionResult.Success(),
            category: "functional",
            tags: ["fp", "legacy"]);

        var output = new RecordingOutputSink();
        var runner = new DemoRunner([dotnet10JsonDemo, dotnet10ExtensionDemo, nonDotNet10Demo], output);
        var opts = new Options { List = true, Tags = ["dotnet10"] };

        // Act
        var result = runner.Execute(opts);

        // Assert
        result.ShouldBeRight();
        output.Lines.Should().Contain("fp-json-strict-validation");
        output.Lines.Should().Contain("fp-extension-members-typeclasses");
        output.Lines.Should().NotContain("legacy-demo");
    }

    [Fact]
    public void ListDemosSeparateEntriesWithBlankLines()
    {
        // Arrange
        var firstDemo = new StubDemo(
            "alpha",
            (_, _) => DemoExecutionResult.Success(),
            category: "functional",
            tags: ["comparison", "fp"],
            description: "First demo");
        var secondDemo = new StubDemo(
            "beta",
            (_, _) => DemoExecutionResult.Success(),
            category: "functional",
            tags: ["comparison", "fp"],
            description: "Second demo");
        var output = new RecordingOutputSink();
        var runner = new DemoRunner([firstDemo, secondDemo], output);

        // Act
        var result = runner.Execute(new Options { List = true });

        // Assert
        result.ShouldBeRight();
        output.Lines.Should().ContainInOrder(
            "== Triad Comparisons ==",
            string.Empty,
            "alpha",
            "  category: functional",
            "  tags: comparison,fp",
            "  description: First demo",
            string.Empty,
            "beta",
            "  category: functional",
            "  tags: comparison,fp",
            "  description: Second demo");
    }

    [Fact]
    public void ListDemosGroupEntriesUnderLearningStageHeaders()
    {
        // Arrange
        var supportingDemo = new StubDemo(
            "patterns",
            (_, _) => DemoExecutionResult.Success(),
            category: "csharp-support",
            tags: ["csharp", "supporting-feature", "baseline"]);
        var baselineDemo = new StubDemo(
            "imperative",
            (_, _) => DemoExecutionResult.Success(),
            category: "imperative",
            tags: ["baseline"]);
        var comparisonDemo = new StubDemo(
            "triad",
            (_, _) => DemoExecutionResult.Success(),
            category: "csharp",
            tags: ["comparison"]);
        var advancedDemo = new StubDemo(
            "advanced",
            (_, _) => DemoExecutionResult.Success(),
            category: "functional",
            tags: ["languageext"]);
        var output = new RecordingOutputSink();
        var runner = new DemoRunner([advancedDemo, comparisonDemo, baselineDemo, supportingDemo], output);

        // Act
        var result = runner.Execute(new Options { List = true });

        // Assert
        result.ShouldBeRight();
        output.Lines.Should().ContainInOrder(
            "== Supporting C# Features ==",
            string.Empty,
            "patterns",
            string.Empty,
            "== Core Baseline Demos ==",
            string.Empty,
            "imperative",
            string.Empty,
            "== Triad Comparisons ==",
            string.Empty,
            "triad",
            string.Empty,
            "== Advanced Functional Topics ==",
            string.Empty,
            "advanced");
    }

    [Fact]
    public void ListDemosWrapLongDescriptionsAcrossIndentedLines()
    {
        // Arrange
        var demo = new StubDemo(
            "wrapped-demo",
            (_, _) => DemoExecutionResult.Success(),
            description: "This description is intentionally long so the console listing wraps it across multiple " +
                         "indented lines instead of forcing the reader to scan a very wide terminal row.");
        var output = new RecordingOutputSink();
        var runner = new DemoRunner([demo], output);

        // Act
        var result = runner.Execute(new Options { List = true });

        // Assert
        result.ShouldBeRight();
        output.Lines.Should().Contain(line => line.StartsWith("  description: "));
        output.Lines.Should().Contain(line => line.StartsWith("               "));
    }
    
    [Theory]
    [AutoData]
    public void ReturnRightForKnownDemo(string knownDemoName)
    {
        // Arrange: stub that always succeeds
        var stub = new StubDemo(knownDemoName, (_, _) => DemoExecutionResult.Success());
        var runner = new DemoRunner([stub]);
        var opts = new Options { Method = knownDemoName, Name = null, Number = null };

        // Act
        var result = runner.Execute(opts);

        // Assert
        result.ShouldBeRight(x => x.IsSuccess.Should().BeTrue());
    }
    
    [Theory]
    [AutoData]
    public void ReturnsLeftWhenThereIsAnError(string knownDemoName, string name, string number, string error)
    {
        // Arrange: stub that fails
        var stub = new StubDemo(knownDemoName, (_, _) => DemoExecutionResult.Failure(error));
        var runner = new DemoRunner([stub]);
        var opts = new Options { Method = knownDemoName, Name = name, Number = number };

        // Act
        var result = runner.Execute(opts);

        // Assert
        result.ShouldBeLeft(msg => msg.Should().Be(error));
    }
    
    [Theory]
    [AutoData]
    public void ReturnsLeftForUnknownDemo(string unknownDemoName)
    {
        // Arrange: runner with no demos registered
        var runner = new DemoRunner([]);
        var opts = new Options { Method = unknownDemoName, Name = null, Number = null };

        // Act
        var result = runner.Execute(opts);

        // Assert
        result.ShouldBeLeft(msg => msg.Should().Contain("Unknown demo"));
    }
}
