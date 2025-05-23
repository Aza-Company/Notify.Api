using Microsoft.AspNetCore.Mvc;
using NotifyHub.Application.Extensions;
using NotifyHub.Domain.Exceptions;

namespace NotifyHub.Api.Middlewares;

public sealed class ErrorHandlerMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ErrorHandlerMiddleware> _logger;

    public ErrorHandlerMiddleware(
        RequestDelegate next,
        ILogger<ErrorHandlerMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleAsync(ex, context);
        }
    }

    private async Task HandleAsync(Exception exception, HttpContext context)
    {
        _logger.LogError(exception, "Exception occurred: {Message}", exception.Message);

        var details = GetErrorDetails(exception);

        context.Response.StatusCode = details.Status!.Value;

        var result = Result.Failure(details); 

        await context.Response
            .WriteAsJsonAsync(result);
    }

    private static ProblemDetails GetErrorDetails(Exception exception)
        => exception switch
    {
        EntityNotFoundException => new ProblemDetails
        {
            Status = StatusCodes.Status404NotFound,
            Title = "Не найдено",
            Detail = exception.Message
        },
        NoActiveDeviceException => new ProblemDetails
        {
            Status = StatusCodes.Status400BadRequest,
            Title = "Нет активного устройства",
            Detail = exception.Message
        },
        Domain.Exceptions.UnauthorizedAccessException => new ProblemDetails
        {
            Status = StatusCodes.Status403Forbidden,
            Title = "Доступ запрещен",
            Detail = exception.Message
        },
        _ => new ProblemDetails
        {
            Status = StatusCodes.Status500InternalServerError,
            Title = "Внутренняя ошибка сервера",
            Detail = exception.Message
        }
    };
}
