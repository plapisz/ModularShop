using Microsoft.AspNetCore.Mvc;

namespace ModularShop.Modules.Identity.Api.Controllers;

[ApiController]
[Route("api/identity")]
internal sealed class HomeController : ControllerBase
{
    [HttpGet]
    public IActionResult Get() 
        => Ok("Identity API");
}