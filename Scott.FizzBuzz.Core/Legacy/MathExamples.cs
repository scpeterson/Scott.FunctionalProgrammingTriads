using LanguageExt;
using static LanguageExt.Prelude;

namespace Scott.FizzBuzz.Core;

public static class MathExamples
{
    public static int Add(int left, int right) => left + right;
    public static int Subtract(int left, int right) => left - right;
    public static int Multiply(int left, int right) => left * right;

    public static Either<string, int> Divide(int left, int right) =>
        right == 0
            ? Left<string, int>("Cannot divide by zero.")
            : Right<string, int>(left / right);
}
