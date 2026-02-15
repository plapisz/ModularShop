namespace ModularShop.Modules.Identity.Core.Dtos;

public sealed record SignInDto
{
    public required string Email { get; init; }
    public required string Password { get; init; }
}