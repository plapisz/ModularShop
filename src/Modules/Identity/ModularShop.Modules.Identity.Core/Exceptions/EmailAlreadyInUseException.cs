using ModularShop.Shared.Abstractions.Exceptions;

namespace ModularShop.Modules.Identity.Core.Exceptions;

public class EmailAlreadyInUseException(string email) : ModularShopException($"Email: '{email}' is already in use.")
{
    public string Email { get; } = email;
}