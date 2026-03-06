using LanguageExt;
using static LanguageExt.Prelude;

namespace Scott.FizzBuzz.Core.Demos.DatabaseTextStoreTriad;

public static class TextStoreRecordCodec
{
    public static Either<string, Seq<PersonRecord>> Parse(string? content)
    {
        if (string.IsNullOrWhiteSpace(content))
            return Right<string, Seq<PersonRecord>>(Empty);

        var rows = new List<PersonRecord>();
        var lines = content.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

        foreach (var line in lines)
        {
            var parsed = ParseLine(line);
            if (parsed.IsLeft)
                return parsed.Map(Seq1);

            parsed.IfRight(record => rows.Add(record));
        }

        return Right<string, Seq<PersonRecord>>(toSeq(rows));
    }

    public static string Serialize(Seq<PersonRecord> rows) =>
        string.Join(Environment.NewLine, rows.Map(SerializeLine));

    public static Seq<PersonRecord> Upsert(Seq<PersonRecord> rows, PersonRecord candidate)
    {
        var withoutExisting = rows.Filter(row => row.Id != candidate.Id);
        return withoutExisting
            .Add(candidate)
            .OrderBy(row => row.Id)
            .ToSeq();
    }

    public static Option<PersonRecord> FindById(Seq<PersonRecord> rows, int id) =>
        rows.Find(row => row.Id == id);

    public static Either<string, PersonRecord> ParseLine(string line)
    {
        var parts = line.Split('|', StringSplitOptions.None);
        if (parts.Length != 3)
            return Left<string, PersonRecord>($"Invalid row format: '{line}'");

        if (!int.TryParse(parts[0], out var id))
            return Left<string, PersonRecord>($"Invalid id value in row: '{line}'");

        var name = parts[1].Trim();
        if (string.IsNullOrWhiteSpace(name))
            return Left<string, PersonRecord>($"Name is required in row: '{line}'");

        if (!int.TryParse(parts[2], out var age))
            return Left<string, PersonRecord>($"Invalid age value in row: '{line}'");

        return Right<string, PersonRecord>(new PersonRecord(id, name, age));
    }

    public static string SerializeLine(PersonRecord record) =>
        $"{record.Id}|{record.Name}|{record.Age}";
}
