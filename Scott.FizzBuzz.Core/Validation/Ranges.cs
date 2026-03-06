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
}
