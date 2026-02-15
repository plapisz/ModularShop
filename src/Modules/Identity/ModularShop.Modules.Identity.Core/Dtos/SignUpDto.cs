namespace ModularShop.Modules.Identity.Core.Dtos;

public sealed record SignUpDto
{
    public required string Email { get; init; }
    public required string Password { get; init; }
    public required string Role { get; init; }
    public required Dictionary<string, IEnumerable<string>> Claims { get; init; }
}