using ModularShop.Modules.Identity.Core.Dtos;
using ModularShop.Modules.Identity.Core.Entities;
using ModularShop.Modules.Identity.Core.Exceptions;
using ModularShop.Modules.Identity.Core.Repositories;
using ModularShop.Modules.Identity.Core.Security;

namespace ModularShop.Modules.Identity.Core.Services;

internal sealed class IdentityService(IUserRepository userRepository, 
    IPasswordManager passwordManager,
    IAuthenticator authenticator)
    : IIdentityService
{
    public async Task<AccountDto?> GetAsync(Guid id)
    {
        var user = await userRepository.GetByIdAsync(id);
        if (user is null)
        {
            return null;
        }

        return new AccountDto
        {
            Id = user.Id,
            Email = user.Email,
            Role = user.Role,
            Claims = user.Claims,
        };
    }

    public async Task SignUpAsync(SignUpDto dto)
    {
        var email = dto.Email.ToLowerInvariant();
        if (await userRepository.GetByEmailAsync(email) is not null)
        {
            throw new EmailAlreadyInUseException(email);
        }
        
        var securedPassword = passwordManager.Secure(dto.Password);
        var user = new User
        {
            Id = Guid.NewGuid(),
            Email = email,
            Password = securedPassword,
            Role = dto.Role.ToLowerInvariant(),
            IsActive = true,
            Claims = dto.Claims,
        };
        await userRepository.AddAsync(user);
    }

    public async Task<JsonWebTokenDto> SignInAsync(SignInDto dto)
    {
        var user = await userRepository.GetByEmailAsync(dto.Email);
        if (user is null || !passwordManager.Validate(dto.Password, user.Password))
        {
            throw new InvalidCredentialsException();
        }

        return authenticator.CreateToken(user.Id, user.Role, user.Claims);
    }
}