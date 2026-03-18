using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ModularShop.Modules.Orders.Api.Request;
using ModularShop.Modules.Orders.Api.Security;
using ModularShop.Modules.Orders.Application.Commands;
using ModularShop.Modules.Orders.Application.Queries;
using ModularShop.Shared.Abstractions.Commands;
using ModularShop.Shared.Abstractions.Queries;

namespace ModularShop.Modules.Orders.Api.Controllers;

[Authorize(Policy = Policies.Order.Read)]
[ApiController]
[Route("api/[controller]")]
public class OrdersController(ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher) : ControllerBase
{
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> Get(Guid id)
    {
        var order = await queryDispatcher.QueryAsync(new GetOrderByIdQuery(id));
        if (order is null)
        {
            return NotFound();
        }

        return Ok(order);
    }
    
    [HttpGet]
    public async Task<IActionResult> BrowseAsync() 
        => Ok(await queryDispatcher.QueryAsync(new BrowseOrdersQuery()));
    
    [HttpGet("customer/{customerId:guid}")]
    public async Task<IActionResult> BrowseCustomerOrdersAsync(Guid customerId) 
        => Ok(await queryDispatcher.QueryAsync(new BrowseCustomerOrdersQuery(customerId)));
    
    [HttpPost]
    [Authorize(Policy = Policies.Order.Write)]
    public async Task<IActionResult> CreateAsync([FromBody] CreateOrderRequest request)
    {
        var orderId = Guid.NewGuid();
        await commandDispatcher.SendAsync(new CreateOrderCommand(orderId, request.CustomerId));
        
        return CreatedAtAction(nameof(Get), new { Id = orderId }, new { id = orderId });
    }

    [HttpPost("{orderId:guid}/items")]
    [Authorize(Policy = Policies.Order.Write)]
    public async Task<IActionResult> AddOrderItem([FromRoute] Guid orderId, [FromBody] AddOrderItemRequest request)
    {
        await commandDispatcher.SendAsync(new AddOrderItemCommand(orderId, request.ProductId, request.Quantity));
        
        return NoContent();
    }

    [HttpDelete("{orderId:guid}/items/{productId:guid}")]
    [Authorize(Policy = Policies.Order.Write)]
    public async Task<IActionResult> RemoveOrderItem([FromRoute] Guid orderId, [FromRoute] Guid productId)
    {
        await commandDispatcher.SendAsync(new RemoveOrderItemCommand(orderId, productId));
        
        return NoContent();
    }

    [HttpPost("{orderId:guid}/confirm")]
    [Authorize(Policy = Policies.Order.Write)]
    public async Task<IActionResult> ConfirmOrder([FromRoute] Guid orderId)
    {
        await commandDispatcher.SendAsync(new ConfirmOrderCommand(orderId));
        
        return NoContent();
    }

    [HttpPost("{orderId:guid}/cancel")]
    [Authorize(Policy = Policies.Order.Write)]
    public async Task<IActionResult> CancelOrder([FromRoute] Guid orderId)
    {
        await commandDispatcher.SendAsync(new CancelOrderCommand(orderId));
        
        return NoContent();
    }
}