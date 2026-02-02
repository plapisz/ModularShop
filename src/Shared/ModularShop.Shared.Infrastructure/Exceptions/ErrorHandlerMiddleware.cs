using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using ModularShop.Shared.Abstractions.Exceptions;

namespace ModularShop.Shared.Infrastructure.Exceptions;

internal sealed class ErrorHandlerMiddleware(
    IEnumerable<IExceptionToResponseMapper> mappers,
    ILogger<ErrorHandlerMiddleware> logger) : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception exception)
        {
            logger.LogError(exception, exception.Message);
            await HandleErrorAsync(context, exception);
        }
    }

    private async Task HandleErrorAsync(HttpContext context, Exception exception)
    {
        var response = mappers
            .Select(mapper => mapper.Map(exception))
            .First();

        context.Response.StatusCode = (int)response.StatusCode;
        await context.Response.WriteAsJsonAsync(response);
    }
}