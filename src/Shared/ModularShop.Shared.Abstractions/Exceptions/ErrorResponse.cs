using System.Net;

namespace ModularShop.Shared.Abstractions.Exceptions;

public sealed record ErrorResponse(Error Response, HttpStatusCode StatusCode);