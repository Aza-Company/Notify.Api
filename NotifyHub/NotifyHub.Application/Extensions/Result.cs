using Microsoft.AspNetCore.Mvc;

namespace NotifyHub.Application.Extensions;

public class Result<T>
{
    public bool IsSuccess { get; private set; }
    public ProblemDetails Error { get; private set; } = null;
    public T? Value { get; private set; }

    public bool IsFailure => !IsSuccess;

    protected Result(bool isSuccess, T? value, ProblemDetails error)
    {
        IsSuccess = isSuccess;
        Value = value;
        Error = error;
    }

    public static Result<T> Success(T value) => new(true, value, null);
    public static Result<T> Failure(ProblemDetails error) => new(false, default, error);
}

public class Result
{
    public bool IsSuccess { get; private set; }
    public ProblemDetails Error { get; private set; } = null;

    public bool IsFailure => !IsSuccess;

    protected Result(bool isSuccess, ProblemDetails error)
    {
        IsSuccess = isSuccess;
        Error = error;
    }

    public static Result Success() => new(true, null);
    public static Result Failure(ProblemDetails error) => new(false, error);
}
