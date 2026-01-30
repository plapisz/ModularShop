using Microsoft.AspNetCore.Mvc;

namespace ModularShop.Modules.Identity.Api.Controllers;

[ApiController]
[Route(IdentityModule.BasePath)]
internal sealed class HomeController : ControllerBase
{
    [HttpGet]
    public IActionResult Get() 
        => Ok("Identity API");
}