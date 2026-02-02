using System.Net;
using ModularShop.Shared.Abstractions.Exceptions;

namespace ModularShop.Shared.Infrastructure.Exceptions;

internal sealed class DefaultExceptionToResponseMapper : IExceptionToResponseMapper
{
    public ErrorResponse Map(Exception exception)
        => exception switch
        {
            ModularShopException ex => new ErrorResponse(
                new Error(GetErrorCode(ex), ex.Message),
                HttpStatusCode.BadRequest),
            _ => new ErrorResponse(
                new Error("error", "There was an error."),
                HttpStatusCode.InternalServerError)
        };
    
    private static string GetErrorCode(Exception exception)
        => exception.GetType().Name.Replace("Exception", "").ToSnakeCase();
}