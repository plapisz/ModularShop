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
    public async Task<IActionResult> GetAsync() 
        => Ok(await identityService.GetAsync(context.Identity.Id));

    [HttpPost("sign-up")]
    public async Task<IActionResult> SignUpAsync(SignUpDto dto)
    {
        await identityService.SignUpAsync(dto);
        return NoContent();
    }

    [HttpPost("sign-in")]
    public async Task<IActionResult> SignInAsync(SignInDto dto)
        => Ok(await identityService.SignInAsync(dto));
}