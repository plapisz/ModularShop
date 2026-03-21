using ModularShop.Modules.Orders.Application.Exceptions;
using ModularShop.Modules.Orders.Domain.Repositories;
using ModularShop.Shared.Abstractions.Commands;

namespace ModularShop.Modules.Orders.Application.Commands.Handlers;

public sealed class RemoveOrderItemCommandHandler(IOrderRepository orderRepository)
    : ICommandHandler<RemoveOrderItemCommand>
{
    public async Task HandleAsync(RemoveOrderItemCommand command, CancellationToken cancellationToken)
    {
        var order = await orderRepository.GetAsync(command.OrderId, cancellationToken);
        if (order is null)
        {
            throw new OrderNotFoundException(command.OrderId);
        }

        order.RemoveItem(command.ProductId);

        await orderRepository.UpdateAsync(order, cancellationToken);
    }
}