using LanguageExt.UnitTesting;
using Scott.FunctionalProgrammingTriads.Core.Validation;
using static LanguageExt.Prelude;

namespace Scott.FunctionalProgrammingTriads.Core.Tests.Validation;

public class RangesValidationsShould
{
    [Fact]
    public void DateRangeReturnSuccessWhenStartIsBeforeEnd()
    {
        var start = new DateTime(2026, 1, 1);
        var end = new DateTime(2026, 1, 2);

        var result = Ranges.DateRange(Some(start), Some(end));

        result.ShouldBeSuccess();
    }

    [Fact]
    public void DateRangeReturnFailWhenStartIsAfterEnd()
    {
        var start = new DateTime(2026, 1, 3);
        var end = new DateTime(2026, 1, 2);

        var result = Ranges.DateRange(Some(start), Some(end));

        result.ShouldBeFail();
    }

    [Fact]
    public void OrderedReturnFailWhenStartIsGreaterThanEnd()
    {
        var result = Ranges.Ordered(5, 1, "Min", "Max");

        result.ShouldBeFail();
    }
}
