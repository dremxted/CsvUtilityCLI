namespace SheetRecordDuplicator.Common.Results;

/// <summary>
/// A result wrapper that contains the state of the result.
/// It has a generic version that allows to store a value, which is accessible if the result is successful.
/// </summary>
public class Result
{
    public readonly bool IsSuccess;
    public readonly Error Error;
    public bool IsFailure => !IsSuccess;
    internal Result(bool isSuccess, Error error)
    {
        if (isSuccess && error != Error.None ||
            !isSuccess && error == Error.None)
        {
            throw new ArgumentException("The Value of the Result is not valid. A Success Result contains an Error or an Error of a Failure Result is set to None", 
                nameof(error));
        }
        IsSuccess = isSuccess;
        Error = error;
    }

    /// <summary>
    /// Invokes <paramref name="success"/> or <paramref name="failure"/> delegate respectively to the state of the result.
    /// </summary>
    /// <typeparam name="TOut"></typeparam>
    /// <param name="success"></param>
    /// <param name="failure"></param>
    /// <returns>The value returned by either the <paramref name="success"/> or <paramref name="failure"/> delegate.</returns>
    public TOut Match<TOut>(Func<TOut> success, Func<Error, TOut> failure)
    {
        return IsSuccess ? success() : failure(Error);
    }

    /// <summary>
    /// A Result factory method.
    /// </summary>
    /// <returns>A new instance of the Result that represent the successful result.</returns>
    public static Result Success()
    {
        return new Result(
            isSuccess: true,
            error: Error.None);
    }

    /// <summary>
    /// A Result factory method.
    /// </summary>
    /// <typeparam name="TOut"></typeparam>
    /// <param name="value"></param>
    /// <returns>A new instance of the Result that represent the successful result with a <paramref name="value"/> parameter.</returns>
    public static Result<TOut> Success<TOut>(TOut value)
    {
        return new Result<TOut>(
            isSuccess: true,
            error: Error.None,
            value: value);
    }

    /// <summary>
    /// A Result factory method.
    /// </summary>
    /// <param name="error"></param>
    /// <returns>A new instance of the Result that represent the failure result with an <paramref name="error"/> parameter.</returns>
    public static Result Failure(Error error)
    {
        return new Result(
            isSuccess: false,
            error: error);
    }

    /// <summary>
    /// A Result factory method.
    /// </summary>
    /// <param name="error"></param>
    /// <returns>A new instance of the Result that represent the failure result with an <paramref name="error"/> parameter.</returns>
    public static Result<TOut> Failure<TOut>(Error error)
    {
        return new Result<TOut>(
            isSuccess: false,
            error: error,
            value: default);
    }
}

/// <summary>
/// A result wrapper with a value property whose type is determined by the generic parameter.
/// </summary>
public class Result<TValue> : Result
{
    private readonly TValue? _value;

    public TValue Value => IsSuccess
        ? _value!
        : throw new InvalidOperationException("The value of a failure result can not be accessed.");

    internal Result(bool isSuccess, Error error, TValue? value) : base(isSuccess, error)
    {
        _value = value;
    }

    /// <summary>
    /// Invokes <paramref name="success"/> or <paramref name="failure"/> delegate respectively to the state of the result.
    /// </summary>
    /// <typeparam name="TOut"></typeparam>
    /// <param name="success"></param>
    /// <param name="failure"></param>
    /// <returns>The value returned by either the <paramref name="success"/> or <paramref name="failure"/> delegate.</returns>
    public TOut Match<TOut>(Func<TValue, TOut> success, Func<Error, TOut> failure)
    {
        return IsSuccess ? success(Value) : failure(Error);
    }
}