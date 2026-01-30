using Microsoft.AspNetCore.Mvc;

namespace ModularShop.Modules.Orders.Api.Controllers;

[ApiController]
[Route(OrdersModule.BasePath)]
internal sealed class HomeController : ControllerBase
{
    [HttpGet]
    public IActionResult Get() 
        => Ok("Orders API");
}