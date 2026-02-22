namespace ModularShop.Modules.Identity.Core.Dtos;

public sealed record AccountDto
{
    public required Guid Id { get; init; }
    public required string Email { get; init; }
    public required string Role { get; init; }
    public required IReadOnlyDictionary<string, IReadOnlyCollection<string>> Claims { get; init; }   
}