using Microsoft.AspNetCore.Mvc;

namespace ModularShop.Modules.Catalog.Api.Controllers;

[ApiController]
[Route(CatalogModule.BasePath)]
internal sealed class HomeController : ControllerBase
{
    [HttpGet]
    public IActionResult Get() 
        => Ok("Catalog API");
}