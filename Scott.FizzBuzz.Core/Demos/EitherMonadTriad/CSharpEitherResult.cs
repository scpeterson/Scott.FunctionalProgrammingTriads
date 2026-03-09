namespace Scott.FizzBuzz.Core.Demos.EitherMonadTriad;

public sealed class CSharpEitherResult<T>
{
    private CSharpEitherResult(T value)
    {
        IsSuccess = true;
        Value = value;
        Error = string.Empty;
    }

    private CSharpEitherResult(string error)
    {
        IsSuccess = false;
        Error = error;
    }

    public bool IsSuccess { get; }
    public T? Value { get; }
    public string Error { get; }

    public static CSharpEitherResult<T> Success(T value) => new(value);

    public static CSharpEitherResult<T> Failure(string error) => new(error);

    public CSharpEitherResult<TResult> Bind<TResult>(Func<T, CSharpEitherResult<TResult>> binder) =>
        IsSuccess && Value is not null
            ? binder(Value)
            : CSharpEitherResult<TResult>.Failure(Error);

    public CSharpEitherResult<TResult> Map<TResult>(Func<T, TResult> mapper) =>
        IsSuccess && Value is not null
            ? CSharpEitherResult<TResult>.Success(mapper(Value))
            : CSharpEitherResult<TResult>.Failure(Error);
}
