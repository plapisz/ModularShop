using ModularShop.Modules.Identity.Core.Dtos;

namespace ModularShop.Modules.Identity.Core.Security;

public interface IAuthenticator
{
    JsonWebTokenDto CreateToken(Guid userId, string role, IDictionary<string, IEnumerable<string>> claims);
}