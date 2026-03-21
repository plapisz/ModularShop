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
    public async Task<IActionResult> Get(Guid id, CancellationToken cancellationToken)
    {
        var order = await queryDispatcher.QueryAsync(new GetOrderByIdQuery(id), cancellationToken);
        if (order is null)
        {
            return NotFound();
        }

        return Ok(order);
    }
    
    [HttpGet]
    public async Task<IActionResult> BrowseAsync(CancellationToken cancellationToken) 
        => Ok(await queryDispatcher.QueryAsync(new BrowseOrdersQuery(), cancellationToken));
    
    [HttpGet("customer/{customerId:guid}")]
    public async Task<IActionResult> BrowseCustomerOrdersAsync(Guid customerId, CancellationToken cancellationToken) 
        => Ok(await queryDispatcher.QueryAsync(new BrowseCustomerOrdersQuery(customerId), cancellationToken));
    
    [HttpPost]
    [Authorize(Policy = Policies.Order.Write)]
    public async Task<IActionResult> CreateAsync([FromBody] CreateOrderRequest request, CancellationToken cancellationToken)
    {
        var orderId = Guid.NewGuid();
        await commandDispatcher.SendAsync(new CreateOrderCommand(orderId, request.CustomerId), cancellationToken);
        
        return CreatedAtAction(nameof(Get), new { Id = orderId }, new { id = orderId });
    }

    [HttpPost("{orderId:guid}/items")]
    [Authorize(Policy = Policies.Order.Write)]
    public async Task<IActionResult> AddOrderItem([FromRoute] Guid orderId, [FromBody] AddOrderItemRequest request, CancellationToken cancellationToken)
    {
        await commandDispatcher.SendAsync(new AddOrderItemCommand(orderId, request.ProductId, request.Quantity), cancellationToken);
        
        return NoContent();
    }

    [HttpDelete("{orderId:guid}/items/{productId:guid}")]
    [Authorize(Policy = Policies.Order.Write)]
    public async Task<IActionResult> RemoveOrderItem([FromRoute] Guid orderId, [FromRoute] Guid productId, CancellationToken cancellationToken)
    {
        await commandDispatcher.SendAsync(new RemoveOrderItemCommand(orderId, productId), cancellationToken);
        
        return NoContent();
    }

    [HttpPost("{orderId:guid}/confirm")]
    [Authorize(Policy = Policies.Order.Write)]
    public async Task<IActionResult> ConfirmOrder([FromRoute] Guid orderId, CancellationToken cancellationToken)
    {
        await commandDispatcher.SendAsync(new ConfirmOrderCommand(orderId), cancellationToken);
        
        return NoContent();
    }

    [HttpPost("{orderId:guid}/cancel")]
    [Authorize(Policy = Policies.Order.Write)]
    public async Task<IActionResult> CancelOrder([FromRoute] Guid orderId, CancellationToken cancellationToken)
    {
        await commandDispatcher.SendAsync(new CancelOrderCommand(orderId), cancellationToken);
        
        return NoContent();
    }
}