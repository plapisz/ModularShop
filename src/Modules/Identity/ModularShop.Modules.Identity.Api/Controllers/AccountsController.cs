using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ModularShop.Modules.Identity.Core.Dtos;
using ModularShop.Modules.Identity.Core.Services;
using ModularShop.Shared.Abstractions.Contexts;

namespace ModularShop.Modules.Identity.Api.Controllers;

[ApiController]
[Route("api/identity/[controller]")]
public class AccountsController(IIdentityService identityService, IContext context) : ControllerBase
{
    [Authorize]
    [HttpGet("me")]
    public async Task<IActionResult> GetAsync(CancellationToken cancellationToken) 
        => Ok(await identityService.GetAsync(context.Identity.Id, cancellationToken));

    [HttpPost("sign-up")]
    public async Task<IActionResult> SignUpAsync(SignUpDto dto, CancellationToken cancellationToken)
    {
        await identityService.SignUpAsync(dto, cancellationToken);
        return NoContent();
    }

    [HttpPost("sign-in")]
    public async Task<IActionResult> SignInAsync(SignInDto dto, CancellationToken cancellationToken)
        => Ok(await identityService.SignInAsync(dto, cancellationToken));
}