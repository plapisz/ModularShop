namespace ModularShop.Modules.Identity.Core.Dtos;

public sealed record JsonWebTokenDto
{
    public required string AccessToken { get; init; }
}