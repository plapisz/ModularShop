using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using ModularShop.Modules.Catalog.Core.Options;

namespace ModularShop.Modules.Catalog.Api.Controllers;

[ApiController]
[Route("api/catalog")]
internal sealed class HomeController(IOptions<CatalogOptions> moduleOptions) : ControllerBase
{
    [HttpGet]
    public IActionResult Get() 
        => Ok($"{moduleOptions.Value.Module.Name} API");
}