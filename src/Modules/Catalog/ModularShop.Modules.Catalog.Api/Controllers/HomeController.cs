using Microsoft.AspNetCore.Mvc;

namespace ModularShop.Modules.Catalog.Api.Controllers;

[ApiController]
[Route("api/catalog")]
internal sealed class HomeController : ControllerBase
{
    [HttpGet]
    public IActionResult Get() 
        => Ok("Catalog API");
}