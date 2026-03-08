using LanguageExt;
using LanguageExt.Common;
using static LanguageExt.Prelude;

namespace Scott.FizzBuzz.Core.Validation;

public static class Ranges
{
    public static Validation<Error, Unit> DateRange(Option<DateTime> start, Option<DateTime> end) =>
        start.Match(
            Some: s => end.Match(
                Some: e => s > e
                    ? Fail<Error, Unit>(Error.New($"The start date '{s}' cannot be after the end date '{e}'."))
                    : Success<Error, Unit>(unit),
                None: () => Fail<Error, Unit>(Error.New("The end date is required."))),
            None: () => Fail<Error, Unit>(Error.New("The start date is required.")));

    public static Validation<Error, (T Start, T End)> Ordered<T>(T start, T end, string startName, string endName)
        where T : IComparable<T> =>
        start.CompareTo(end) <= 0
            ? Success<Error, (T Start, T End)>((start, end))
            : Fail<Error, (T Start, T End)>(Error.New($"'{startName}' cannot be greater than '{endName}'."));
}
