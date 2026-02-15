namespace ModularShop.Modules.Identity.Core.Entities;

public class User
{
    public required Guid Id { get; init; }
    public required string Email { get; set; }
    public required string Password { get; set; }
    public required string Role { get; set; }
    public required bool IsActive { get; set; }
    public required Dictionary<string, IEnumerable<string>> Claims { get; set; }   
}