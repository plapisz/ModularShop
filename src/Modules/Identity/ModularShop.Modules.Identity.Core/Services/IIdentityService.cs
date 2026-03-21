using ModularShop.Modules.Identity.Core.Dtos;

namespace ModularShop.Modules.Identity.Core.Services;

public interface IIdentityService
{
    Task<AccountDto?> GetAsync(Guid id, CancellationToken cancellationToken);
    Task SignUpAsync(SignUpDto dto, CancellationToken cancellationToken);
    Task<JsonWebTokenDto> SignInAsync(SignInDto dto, CancellationToken cancellationToken);
}