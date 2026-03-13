using LanguageExt.UnitTesting;
using Scott.FunctionalProgrammingTriads.Core.Demos.DatabaseTextStoreTriad;
using static LanguageExt.Prelude;

namespace Scott.FunctionalProgrammingTriads.Core.Tests.Demos.DatabaseTextStoreTriad;

public class TextStoreRecordCodecShould
{
    [Fact]
    public void ParseAndSerializeRoundTripValidRows()
    {
        var input = "1|Alice|30" + Environment.NewLine + "2|Bob|41";

        var parsed = TextStoreRecordCodec.Parse(input);
        parsed.ShouldBeRight();

        var serialized = parsed.Match(
            Right: TextStoreRecordCodec.Serialize,
            Left: _ => string.Empty);

        Assert.Equal(input, serialized);
    }

    [Fact]
    public void ReturnLeftForInvalidRowFormat()
    {
        var result = TextStoreRecordCodec.Parse("1|Alice");

        result.ShouldBeLeft();
    }

    [Fact]
    public void UpsertReplaceByIdAndKeepSortOrder()
    {
        var rows = Seq(
            new PersonRecord(2, "Bob", 40),
            new PersonRecord(1, "Alice", 30));

        var updated = TextStoreRecordCodec.Upsert(rows, new PersonRecord(1, "Alice", 31));

        Assert.Equal(31, updated.Head().Age);
        Assert.Equal([1, 2], updated.Map(row => row.Id).ToArray());
    }
}
