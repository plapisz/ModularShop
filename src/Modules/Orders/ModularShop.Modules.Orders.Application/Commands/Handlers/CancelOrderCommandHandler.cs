using ModularShop.Modules.Orders.Application.Exceptions;
using ModularShop.Modules.Orders.Domain.Repositories;
using ModularShop.Shared.Abstractions.Commands;

namespace ModularShop.Modules.Orders.Application.Commands.Handlers;

public sealed class CancelOrderCommandHandler(IOrderRepository orderRepository)
    : ICommandHandler<CancelOrderCommand>
{
    public async Task HandleAsync(CancelOrderCommand command, CancellationToken cancellationToken)
    {
        var order = await orderRepository.GetAsync(command.OrderId, cancellationToken);
        if (order is null)
        {
            throw new OrderNotFoundException(command.OrderId);
        }
        
        order.Cancel();
        
        await orderRepository.UpdateAsync(order, cancellationToken);
    }
}