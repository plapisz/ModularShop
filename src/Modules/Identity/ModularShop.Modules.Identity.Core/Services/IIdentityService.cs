using ModularShop.Modules.Identity.Core.Dtos;

namespace ModularShop.Modules.Identity.Core.Services;

public interface IIdentityService
{
    Task<AccountDto?> GetAsync(Guid id);
    Task SignUpAsync(SignUpDto dto);
    Task<JsonWebTokenDto> SignInAsync(SignInDto dto);
}