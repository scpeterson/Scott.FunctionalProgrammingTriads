namespace Scott.FunctionalProgrammingTriads.Core.Demos.OptionMonadTriad;

public static class CSharpNullableBindExtensions
{
    // This helper exists to show the extra plumbing needed when emulating Option-like composition with nullable refs.
    public static TResult? Bind<T, TResult>(this T? value, Func<T, TResult?> binder)
        where T : class
        where TResult : class
        => value is null ? null : binder(value);
}
