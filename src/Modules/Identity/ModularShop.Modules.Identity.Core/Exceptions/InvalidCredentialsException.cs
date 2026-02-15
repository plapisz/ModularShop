using ModularShop.Shared.Abstractions.Exceptions;

namespace ModularShop.Modules.Identity.Core.Exceptions;

public sealed class InvalidCredentialsException() : ModularShopException("Invalid credentials.");