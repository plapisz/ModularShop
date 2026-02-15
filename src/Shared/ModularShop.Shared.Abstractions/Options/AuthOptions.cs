namespace ModularShop.Shared.Abstractions.Options;

public sealed class AuthOptions
{
    public required string Issuer { get; set; }
    public required string Audience { get; set; }
    public required string SigningKey { get; set; }
    public required TimeSpan Expiry { get; set; }
}