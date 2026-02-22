using ModularShop.Modules.Identity.Core.Dtos;

namespace ModularShop.Modules.Identity.Core.Security;

public interface IAuthenticator
{
    JsonWebTokenDto CreateToken(Guid userId, string role, IReadOnlyDictionary<string, IReadOnlyCollection<string>> claims);
}